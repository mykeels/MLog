using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace MLog.Core
{
    public class Util
    {
        public static string GetIpAddress(HttpContext context = null)
        {
            if (context != null && context.Connection != null)
            {
                return context.Connection.RemoteIpAddress.ToString();
            }
            return "127.0.0.1";
        }

        public static string GetMethodFullName(int framesToSkip = 1)
        {
            return "Not Implemented";
        }

        public static string GetAppSettings(string key)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("config.json", optional: true, reloadOnChange: true);
            IConfigurationRoot Configuration = builder.Build();
            return Configuration[key];
        }
    }
}
