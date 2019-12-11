using WebApi.Common.Helper;
using WebApi.Common.KeyVault.Attributes;

namespace WebApi.Conversion4.Models.Data
{
    public class AzureKeyVaultProvider
    {
        const string SITE_NAME_MARK = "{SiteName}";
        const string DB_SERVER_NAME_MARK = "{DBConnectionServerName}";

        public static string AESKeyBLOB  => ConversionConfig.GetValue("AESKeyBLOB");
        public static string CustomExportQueue => ConversionConfig.GetValue("CustomExportQueue");
        public static string CreateCustomExportQueue(string siteName)
        {
            return CustomExportQueue?.Replace(SITE_NAME_MARK, siteName);
        }

        public static string CustomExportBlobContainer => ConversionConfig.GetValue("CustomExportBlobContainer");
        public static string CreateCustomExportBlobContainer(string siteName)
        {
            return CustomExportBlobContainer?.Replace(SITE_NAME_MARK, siteName);
        }
        public static string SendGridApiKey => ConversionConfig.GetValue("SendGridApiKey");

        #region Sql server
        private static string SuffixKeySet_Sql => ConversionConfig.GetValue("SuffixKeySet:Sql");
        public static string DBConnectionServerName => ConversionConfig.GetValue($"DBConnectionServerName{SuffixKeySet_Sql}");
        public static string DBConnectionUserName => ConversionConfig.GetValue("DBConnectionUserName");
        public static string DBConnectionPassword => ConversionConfig.GetValue("DBConnectionPassword");
        public static string DBConnectionStringTemplate => ConversionConfig.GetValue("DBConnectionStringTemplate");
        public static string CreateCustomerDbConnectionString(string siteName)
        {
            return DBConnectionStringTemplate?.Replace(SITE_NAME_MARK, siteName).Replace(DB_SERVER_NAME_MARK, DBConnectionServerName);
        }
        #endregion

        #region Storage
        private static string SuffixKeySet_Storage => ConversionConfig.GetValue("SuffixKeySet:Storage");
        public static string StorageAccountName => ConversionConfig.GetValue($"StorageAccountName{SuffixKeySet_Storage}");
        public static string StorageAccountKey  => ConversionConfig.GetValue($"StorageAccountKey{SuffixKeySet_Storage}");
        //NonVault
        public static string StorageConnectionString
        => $"DefaultEndpointsProtocol=https;AccountName={StorageAccountName};AccountKey={StorageAccountKey}";
        public static string LogStorageConnectionString => ConversionConfig.GetValue("LogStorageConnectionString");
        #endregion

        #region service bus
        private static string SuffixKeySet_ServiceBus => ConversionConfig.GetValue("SuffixKeySet:ServiceBus");
        public static string ServiceBusNamespace => ConversionConfig.GetValue($"ServiceBusNamespace{SuffixKeySet_ServiceBus}");
        public static string ServiceBusAccessKey => ConversionConfig.GetValue($"ServiceBusAccessKey{SuffixKeySet_ServiceBus}");
        public static string ServiceBusAccessKeyName => ConversionConfig.GetValue($"ServiceBusAccessKeyName{SuffixKeySet_ServiceBus}");
        //[NonVault]
        public static string ServiceBusConnectionString()
            => $"Endpoint=sb://{ServiceBusNamespace}.servicebus.windows.net/;SharedAccessKeyName={ServiceBusAccessKeyName};SharedAccessKey={ServiceBusAccessKey}";

        #endregion

        public class CoreVault
        {
            public static string AESSecretKey => ConversionConfig.GetValue("AESSecretKey");
            public static string IOConnectorAESKeyBLOB => ConversionConfig.GetValue("IOConnectorAESKeyBLOB");
            public static string DBConnectionServerName => ConversionConfig.GetValue("DBConnectionServerName");
            public static string DBConnectionUserName => ConversionConfig.GetValue("DBConnectionUserName");
            public static string DBConnectionPassword => ConversionConfig.GetValue("DBConnectionPassword");
            public static string DBConnectionDBName => ConversionConfig.GetValue("DBConnectionDBName");
            public static string StorageAccountNameShared => ConversionConfig.GetValue("StorageAccountNameShared");
            public static string SharedApiVCardWebForWexEndpoint => ConversionConfig.GetValue("SharedApiVCardWebForWexEndpoint");
            public static string SharedApiVCardWebForWexKey => ConversionConfig.GetValue("SharedApiVCardWebForWexKey");
            /// <summary>
            /// Encrypted
            /// </summary>
            public static string StorageAccountKeyShared => ConversionConfig.GetValue("SharedApiVCardWebForWexKey");
            //[NonVault]
            public static string StorageSharedConnectionString
        => $"DefaultEndpointsProtocol=https;AccountName={StorageAccountNameShared};AccountKey={AESHelper.DecryptAES(StorageAccountKeyShared, AESSecretKey)}";
        }
    }
}
