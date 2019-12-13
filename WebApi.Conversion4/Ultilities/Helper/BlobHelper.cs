using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Serilog;
using System.IO;
using System.Text;

namespace WebApi.Conversion4.Ultilities.Helper
{
    public static class BlobHelper
    {
        private readonly static ILogger _logger = Log.ForContext(typeof(BlobHelper));

        public static byte[] DownloadFileToArrayByte(string blobConnectionString, string blobContainerName, string blobName, string decryptKey = null)
        {
            // Retrieve reference to a previously created container.
            CloudBlobContainer blobContainer = GetCloudBlobContainer(blobConnectionString, blobContainerName);
            if (blobContainer == null || !blobContainer.ExistsAsync().Result) throw new System.ArgumentException(string.Format("Can't find the BLOB container [{0}].", blobContainerName));
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blobFile = blobContainer.GetBlockBlobReference(blobName);
            if (blobFile == null || !blobFile.ExistsAsync().Result) throw new System.ArgumentException(string.Format("Can't find the BLOB name [{0}].", blobName));

            return DownloadFileToArrayByte(blobFile, decryptKey);
        }

        public static CloudBlobContainer GetCloudBlobContainer(string blobConnectionString, string blobContainerName)
        {
            if (string.IsNullOrEmpty(blobConnectionString)) throw new System.ArgumentException("Blob connection string is required.");
            //Get the file from BLOB Storage 
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(blobConnectionString);
            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            // Retrieve reference to a previously created container.
            CloudBlobContainer blobContainer = blobClient.GetContainerReference(blobContainerName);

            return blobContainer;
        }
        
        public static byte[] DownloadFileToArrayByte(CloudBlockBlob blobFile, string decryptKey = null)
        {
            byte[] aFile = null;
            if (blobFile != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    blobFile.DownloadToStreamAsync(ms);
                    if (ms != null)
                    {
                        aFile = ms.ToArray();
                        if (!string.IsNullOrEmpty(decryptKey)) aFile = AESHelper.DecryptAES(aFile, decryptKey);
                    }                    
                }
            }            
            return aFile;
        }

        //upload file
        public static void UploadFile(string blobConnectionString, string blobContainerName, string blobName, string blobContentFilePath, string encryptKey = null)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainer(blobConnectionString, blobContainerName);
            //byte[] aBlobContent = File.ReadAllBytes(blobContentFilePath);
            StringBuilder aBlobContent = new StringBuilder(blobContentFilePath);
            byte[] aOutputBlobContent = null;
            //encrypt with AESKey
            if (!string.IsNullOrEmpty(encryptKey))
                aOutputBlobContent = AESHelper.EncryptAES(aBlobContent, encryptKey);

            UploadFile(blobContainer, blobName, aOutputBlobContent);
        }

        public static void UploadFile(string blobConnectionString, string blobContainerName, string blobName, StringBuilder blobContent, string encryptKey = null)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainer(blobConnectionString, blobContainerName);
            UploadFile(blobContainer, blobName, blobContent, encryptKey);
        }
        public static void UploadFile(string blobConnectionString,string blobContainerName, string blobName, byte[] arrbytes, string encryptKey = null)
        {
            CloudBlobContainer blobContainer = GetCloudBlobContainer(blobConnectionString, blobContainerName);

            StringBuilder BlobContent = new StringBuilder(arrbytes.ToString());
            byte[] aOutputBlobContent = null;
            //encrypt with AESKey
            if (!string.IsNullOrEmpty(encryptKey))
                aOutputBlobContent = AESHelper.EncryptAES(BlobContent, encryptKey);

            UploadFile(blobContainer, blobName, aOutputBlobContent);

        }
        public static void UploadFile(CloudBlobContainer blobContainer, string blobName, StringBuilder blobContent, string encryptKey = null)
        {
            string inputString = string.Empty;
            if (blobContent != null) inputString = blobContent.ToString();
            //byte[] aBlobContent = Encoding.UTF8.GetBytes(inputString);
            StringBuilder aBlobContent = new StringBuilder(inputString);
            //encrypt with AESKey
            byte[] aOutputBlobContent = null;
            if (!string.IsNullOrEmpty(encryptKey))
                aOutputBlobContent = AESHelper.EncryptAES(aBlobContent, encryptKey);

            UploadFile(blobContainer, blobName, aOutputBlobContent);
        }
        
        public static void UploadFile(CloudBlobContainer blobContainer, string blobName, byte[] aBlobContent)
        {
            //validate parameter
            if (blobContainer == null || !blobContainer.ExistsAsync().Result) throw new System.ArgumentException("Blob container is required.");
            if (string.IsNullOrEmpty(blobName)) throw new System.ArgumentException("Blob name is required.");

            CloudBlockBlob blockBlobOutput = blobContainer.GetBlockBlobReference(blobName);
            using (var ms = new MemoryStream(aBlobContent, true))
            {
                blockBlobOutput.UploadFromStreamAsync(ms);
                
                _logger.Information($"File [{0}] uploaded to the Blob container [{1}] successfully.", blobName, blobContainer.Name);
            }
        }
    }
}
