﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Serilog;
using WebApi.Conversion4.Ultilities.Helper;
using WebApi.Conversion4.Models;
using WebApi.Conversion4.Ultilities.KeyVault;
using WebApi.Conversion4.Models.Provider;

namespace WebApi.Conversion4.Services
{
    internal class SupplierConversionService
    {
        private readonly PsTool _psTool;
        private readonly ILogger _logger = Log.ForContext(typeof(SupplierConversionService));

        internal SupplierConversionService(PsTool psTool)
        {
            _psTool = psTool;
        }
        // request nay la send từ postman, qua hàm này request.Content.ReadAsMultipartAsync 
        // nó sẽ lầy tên file từ body truyền trên postman
        // System.net.http ko xa2i tre6n .netcore
        // Doc them phan Upload large files with streaming
        // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-3.0
        internal MessageResponse ProcessHttpRequest(MasterConversionRequest request)
        {
            try
            {
                // coi them vd cho .net framework 
                // https://stackoverflow.com/questions/14937926/file-name-from-httprequestmessage-content

                //if (!request.Content.IsMimeMultipartContent())
                //    return MessageResponse.info("Invalid media type - multipart");
                //var provider = new MultipartFormDataMemoryStreamProvider();
                //await request.Content.ReadAsMultipartAsync(provider);
                //var inputContent = GetInputFileContentAndLogRequest(provider);
                if (request is null || request.FileData.Length == 0) //TODO length > maximum length
                {
                    return MessageResponse.error("Bad request. File not found!");
                }

                var inputContent = GetInputFileContentAndLogRequest(request);

                var sb = _psTool.ProcessDataFile(inputContent);

                var errorList = _psTool.GetErrors();
                if (errorList.Any())
                    return MessageResponse.error(errorList);

                var encryptedData = AESHelper.EncryptAES(sb, AzureKeyVaultProvider.AESKeyBLOB);
                return MessageResponse.ok(encryptedData);
            }
            catch (Exception e)
            {
                _logger.Error(e.Message);
                return MessageResponse.error(e.Message);
            }

        }

        private byte[] GetInputFileContentAndLogRequest(MasterConversionRequest request)
        {
            var isGetFromBlob = false;

            // Show all the key-value pairs.
            if (!string.IsNullOrEmpty(request.CaseAction))
                _logger.Information("CaseAction: " + request.CaseAction);
            if (!string.IsNullOrEmpty(request.ContainerName))
                _logger.Information("containerName: " + request.ContainerName);
            if (!string.IsNullOrEmpty(request.BlobName))
                _logger.Information("blobName: " + request.BlobName);
            if (!string.IsNullOrEmpty(request.Decrypt))
                _logger.Information("decrypt: " + request.Decrypt);
            if (!string.IsNullOrEmpty(request.SiteName))
                _logger.Information("SiteName: " + request.SiteName);

            isGetFromBlob = string.IsNullOrEmpty(request.isGetFromBlob) ? false : bool.Parse(request.isGetFromBlob);
            _logger.Information("isGetFromBlob : " + request.isGetFromBlob);

            var byteArr = GetByteFile(request.FileData, isGetFromBlob, request.ContainerName, request.BlobName);
            return byteArr;
        }

        private byte[] GetByteFile(IFormFile fileData, bool isGetFromBlob, string containerName, string blobName)
        {
            var decryptKey = AzureKeyVaultProvider.AESKeyBLOB;
            // tại sao bên đây có case này còn bên payment thì default là down luon?
            if (isGetFromBlob)
            {
                //Non Vault
                var storageConnectionString = AzureKeyVaultProvider.StorageConnectionString;
                return BlobHelper.DownloadFileToArrayByte(storageConnectionString, containerName, blobName, decryptKey);
            }
            // register to read content of excel file
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            byte[] fileContent = null;
            using (var ms = new MemoryStream())
            {
                fileData.CopyTo(ms);
                fileContent = ms.ToArray();
            }
            return AESHelper.DecryptAES(fileContent, decryptKey);
        }
    }
}
