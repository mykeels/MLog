using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace MLog
{
    public class Log
    {
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

        public tblLog Map()
        {
            tblLog ret = new tblLog();
            ret.Application = this.Application;
            ret.CreationDate = Convert.ToDateTime(this.CreationDate);
            ret.Description = this.Description;
            ret.ID = this.ID;
            ret.IpAddress = this.IpAddress;
            ret.JsonData = this.JsonData;
            ret.Parent = Convert.ToInt64(this.Parent);
            ret.Title = this.Title;
            ret.XmlData = this.XmlData;
            ret.MethodName = this.MethodName;
            ret.StackTrace = this.StackTrace;
            return ret;
        }

        public static Log Map(tblLog log)
        {
            if (log == null) return null;
            Log ret = new Log();
            ret.Application = log.Application;
            ret.CreationDate = Convert.ToDateTime(log.CreationDate);
            ret.Description = log.Description;
            ret.ID = log.ID;
            ret.IpAddress = log.IpAddress;
            ret.JsonData = log.JsonData;
            ret.Parent = Convert.ToInt64(log.Parent);
            ret.Title = log.Title;
            ret.XmlData = log.XmlData;
            ret.MethodName = log.MethodName;
            ret.StackTrace = log.StackTrace;
            return ret;
        }

        private Log()
        {
            this.Application = Convert.ToString((string)ConfigurationManager.AppSettings["mlog-app"]);
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