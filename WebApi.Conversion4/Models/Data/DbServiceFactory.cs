using WebApi.Conversion4.Ultilities.Config;
using WebApi.Conversion4.Ultilities.Helper;
using WebApi.Conversion4.Ultilities.KeyVault;

namespace WebApi.Conversion4.Models.Data
{
    internal class DbServiceFactory
    {

        const string SITE_NAME_MARK = "{SiteName}";
        const string DB_SERVER_NAME_MARK = "{DBConnectionServerName}";

        internal static BaseDao GetDao(string sitename)
        {
            string connectionString = AzureKeyVaultProvider.DBConnectionStringTemplate?.Replace(SITE_NAME_MARK, sitename).Replace(DB_SERVER_NAME_MARK, AzureKeyVaultProvider.DBConnectionServerName);
            return new BaseDao(connectionString, AzureKeyVaultProvider.DBConnectionUserName, AzureKeyVaultProvider.DBConnectionPassword);
        }
        internal static BaseDao GetCurrent()
        {
            var siteName = ConversionConfig.GetValue("SiteName");
            return GetDao(siteName);
        }

        internal static BaseDao GetDao(string dbName, string userName, string password)
        {
            string connectionString = AzureKeyVaultProvider.DBConnectionStringTemplate?.Replace(SITE_NAME_MARK, dbName).Replace(DB_SERVER_NAME_MARK, AzureKeyVaultProvider.DBConnectionServerName);
            return new BaseDao(connectionString, userName, password);
        }
    }
}