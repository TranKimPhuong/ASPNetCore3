using Microsoft.Extensions.Configuration;

namespace WebApi.Conversion4.Ultilities.Config
{
    public static class ConversionConfig
    {
        public static IConfiguration Configuration;

        public static string GetValue(string key)
        {
            return Configuration[key];
        }
    }
}
