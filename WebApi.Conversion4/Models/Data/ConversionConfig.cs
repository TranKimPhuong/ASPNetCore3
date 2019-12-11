using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace WebApi.Conversion4.Models.Data
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
