using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver.Linq;
using MongoDB.Bson;

namespace MLog.Core
{
    public class Log
    {
        public ObjectId Id { get; set; }
        public long ID { get; set; }
        public long Parent { get; set; }
        public string Application { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string JsonData { get; set; }
        public XElement XmlData { get; set; }
        public string IpAddress { get; set; }
        private string MethodName { get; set; }
        private string StackTrace { get; set; }

        private IConfigurationRoot Configuration { get; set; }

        private Log()
        {
            this.Application = Convert.ToString((string)Util.GetAppSettings("appSettings:mlog-app"));
            this.CreationDate = DateTime.Now;
        }

        public static Log Create<T>(T data)
        {
            Log ret = new Log();
            ret.JsonData = JsonConvert.SerializeObject(data);
            ret.XmlData = data.ToXElement();
            ret.MethodName = Util.GetMethodFullName(2);
            ret.StackTrace = Environment.StackTrace;
            return ret;
        }

        public static Log Create()
        {
            Log ret = new Log();
            ret.MethodName = Util.GetMethodFullName(2);
            ret.StackTrace = Environment.StackTrace;
            return ret;
        }

        public T GetJsonObject<T>()
        {
            return JsonConvert.DeserializeObject<T>(this.JsonData);
        }

        public dynamic GetJsonObject()
        {
            return JsonConvert.DeserializeObject(this.JsonData);
        }

        public T GetXmlData<T>()
        {
            return this.XmlData.ToObject<T>();
        }

        public List<Log> GetChildren()
        {
            return Manager.GetLogs(new Log()
            {
                Parent = this.ID
            });
        }

        public Log AddChild(Log log)
        {
            return Manager.Add(log, this);
        }

        public string GetMethodName()
        {
            return this.MethodName;
        }

        public string GetStackTrace()
        {
            return this.StackTrace;
        }
    }
}
