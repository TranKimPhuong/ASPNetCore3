using Microsoft.Extensions.Configuration;
using WebApi.Common.Helper;
using WebApi.Common.KeyVault.Attributes;

namespace WebApi.Common.KeyVault
{
    public class VaultNew
    {
        // ko bit lam sao la61y properties trong IConfiguration ra mà chỉ cần truyền IConfiguration 1 lần
        // nên xài trực tiếp luôn cho lẹ
        private readonly IConfiguration _configuration;
        public VaultNew(IConfiguration configuration)
        {
            _configuration = configuration;
            InitalizeVault();
        }

        // coi lại cách lấy data từ properties
        private void InitalizeVault() {
            AESKeyBLOB = _configuration["AESKeyBLOB"];
            // SQL server
            DBConnectionServerName = _configuration["DBConnectionServerName"];
            DBConnectionUserName = _configuration["DBConnectionUserName"];
            DBConnectionPassword = _configuration["DBConnectionPassword"];
            DBConnectionStringTemplate = _configuration["DBConnectionStringTemplate"];

            // Storage
            StorageAccountName = _configuration["StorageAccountName"];
            StorageAccountKey = _configuration["StorageAccountKey"];
            StorageConnectionString = $"DefaultEndpointsProtocol=https;AccountName={StorageAccountName};AccountKey={StorageAccountKey}";
            LogStorageConnectionString = _configuration["LogStorageConnectionString"];

            //service bus
            ServiceBusNamespace = _configuration["ServiceBusNamespace"];
            ServiceBusAccessKey = _configuration["ServiceBusAccessKey"];
            ServiceBusAccessKeyName = _configuration["ServiceBusAccessKeyName"];
            ServiceBusConnectionString = $"Endpoint=sb://{ServiceBusNamespace}.servicebus.windows.net/;SharedAccessKeyName={ServiceBusAccessKeyName};SharedAccessKey={ServiceBusAccessKey}";
        }

        const string SITE_NAME_MARK = "{SiteName}";
        const string DB_SERVER_NAME_MARK = "{DBConnectionServerName}";

        public string AESKeyBLOB { get; set; }


        #region Sql server
        [VariousSuffixKeySet(SuffixKeySet.Sql)]
        public string DBConnectionServerName { get; set; }
        public string DBConnectionUserName { get; set; }
        public string DBConnectionPassword { get; set; }
        public string DBConnectionStringTemplate { get; set; }
        public string CreateCustomerDbConnectionString(string siteName)
        {
            return DBConnectionStringTemplate?.Replace(SITE_NAME_MARK, siteName).Replace(DB_SERVER_NAME_MARK, DBConnectionServerName);
        }
        #endregion

        #region Storage
        [VariousSuffixKeySet(SuffixKeySet.Storage)]
        public string StorageAccountName { get; set; }
        [VariousSuffixKeySet(SuffixKeySet.Storage)]
        public string StorageAccountKey { get; set; }
        [NonVault]
        public string StorageConnectionString { get; set; }
        public string LogStorageConnectionString { get; set; }
        #endregion

        #region service bus
        [VariousSuffixKeySet(SuffixKeySet.ServiceBus)]
        public string ServiceBusNamespace { get; set; }
        [VariousSuffixKeySet(SuffixKeySet.ServiceBus)]
        public string ServiceBusAccessKey { get; set; }
        [VariousSuffixKeySet(SuffixKeySet.ServiceBus)]
        public string ServiceBusAccessKeyName { get; set; }
        [NonVault]
        public string ServiceBusConnectionString { get; set; }
        #endregion

        public class CoreVaultNew
        {
            private readonly IConfiguration _configuration;

            public CoreVaultNew(IConfiguration configuration)
            {
                _configuration = configuration;
                InitilizeCoreVault();
            }

            private void InitilizeCoreVault()
            {
                AESSecretKey = _configuration["AESSecretKey"];
                IOConnectorAESKeyBLOB = _configuration["IOConnectorAESKeyBLOB"];
                DBConnectionServerName = _configuration["DBConnectionServerName"];
                DBConnectionUserName = _configuration["DBConnectionUserName"];
                DBConnectionPassword = _configuration["DBConnectionPassword"];
                DBConnectionDBName = _configuration["DBConnectionDBName"];
                StorageAccountNameShared = _configuration["StorageAccountNameShared"];
                SharedApiVCardWebForWexEndpoint = _configuration["SharedApiVCardWebForWexEndpoint"];
                SharedApiVCardWebForWexKey = _configuration["SharedApiVCardWebForWexKey"];
                StorageAccountKeyShared = _configuration["StorageAccountKeyShared"];
                StorageSharedConnectionString = $"DefaultEndpointsProtocol=https;AccountName={StorageAccountNameShared};AccountKey={AESHelper.DescryptAES(StorageAccountKeyShared, AESSecretKey)}";

            }
            public string AESSecretKey { get; set; }
            public string IOConnectorAESKeyBLOB { get; set; }
            public string DBConnectionServerName { get; set; }
            public string DBConnectionUserName { get; set; }
            public string DBConnectionPassword { get; set; }
            public string DBConnectionDBName { get; set; }
            public string StorageAccountNameShared { get; set; }
            public string SharedApiVCardWebForWexEndpoint { get; set; }
            public string SharedApiVCardWebForWexKey { get; set; }
            /// <summary>
            /// Encrypted
            /// </summary>
            public string StorageAccountKeyShared { get; set; }
            [NonVault]
            public string StorageSharedConnectionString { get; set; }
            
        }
    }
}
