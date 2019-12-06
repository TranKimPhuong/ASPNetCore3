using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.SerilogDemo.Test
{
    public static class TestConfigManager
    {

        public static IConfiguration Configuration;

        public static string GetValue(string key)
        {
            return Configuration[key];
        }
    }
}
