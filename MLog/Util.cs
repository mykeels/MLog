using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace MLog
{
    public class Util
    {
        public static string GetIpAddress()
        {
            if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
            {
                return System.Net.Dns.GetHostAddresses(System.Web.HttpContext.Current.Request.UserHostAddress).GetValue(0).ToString();
            }
            return 
        }
    }
}
