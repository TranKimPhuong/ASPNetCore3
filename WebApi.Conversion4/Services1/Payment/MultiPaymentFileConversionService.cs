using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Common.Helper;
using WebApi.Common.KeyVault;
using WebApi.Common.Models;
using WebApi.Conversion3.Models.Data.Provider;
using BrokeredMessageProperties = System.Collections.Generic.Dictionary<string, string>;
using FileDataCollection = System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<string, byte[]>>;
//TODO
namespace WebApi.Conversion3.Services.Payment
{
    public class MultiPaymentFileConversionService
    {
        private readonly PsTool _psTool;
        public MultiPaymentFileConversionService(PsTool psTool)
        {
            _psTool = psTool;
        }
        public async Task<MessageResponse> ProcessHttpRequest(HttpRequestMessage request)
        {
            if (!request.Content.IsMimeMultipartContent())
                return MessageResponse.info("Invalid media type - multipart");

            var provider = new MultipartFormDataMemoryStreamProvider();
            await request.Content.ReadAsMultipartAsync(provider);
            var inputFiles = provider.FileData;

            var primaryFilter = "*.*"; //TODO: get from httpRequest
            var primaryFiles = GetInputFiles(provider, primaryFilter);

            var remitFilter = "*.*"; //TODO: get from httpRequest
            var remittanceFiles = GetInputFiles(provider, remitFilter);

            var sbStandardFile = _psTool.ProcessMultiDataFile(primaryFiles, remittanceFiles);
            var listErrors = _psTool.GetErrors();

            if (listErrors.Any())
                return MessageResponse.error(listErrors);

            //Upload to storage
            var storageConnString = Vault.Current.StorageConnectionString;
            var decryptKey = Vault.Current.AESKeyBLOB;
            var containerName = "containerName";//TODO: where?
            var blobOutputName = "blobOutputName";//TODO: how?
            BlobHelper.UploadFile(storageConnString, containerName, blobOutputName, sbStandardFile, decryptKey);

            //Send msg to importWorker
            var messageProperties = new BrokeredMessageProperties();
            messageProperties.Add("siteName", "SiteName");
            var sbConnectionString = Vault.Current.ServiceBusConnectionString;
            var queueName = "queueName";//TODO:Where?
            MessageHelper.SendMessageToServiceBus(sbConnectionString, queueName, messageProperties);

            return MessageResponse.ok(null);

        }
        FileDataCollection GetInputFiles(MultipartFormDataMemoryStreamProvider provider, string pattern)
        {
            var inputFiles = provider.FileData;
            var matchedFileNames = inputFiles.Select(s => s.Key).Search(pattern);
            var result = inputFiles.FindAll(s => matchedFileNames.Contains(s.Key));
            return result;
        }
    }
}
