using log4net;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Reflection;

namespace WebApi.Common.Helper
{
    public class ConfigHelper
    {
        ILog LOGGER = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        readonly IConfiguration _configuration;

        ConfigHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public static string GetString(string Key)
        {
            return AppHelper.ToString(ConfigurationManager.AppSettings[Key]);
        }
        public static int GetInt(string Key)
        {
            return AppHelper.ToInt(GetString(Key));
        }
    }    
}
