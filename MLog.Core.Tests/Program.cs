using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MLog.Core;

namespace MLog.Core.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            _createLogsAndRelationships();
            //Log log = Log.Create();
            //Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(log));
            Console.Read();
        }

        private static void _createLogsAndRelationships()
        {
            var logs = new List<Log>();
            for (int i = 1; i <= 5; i++)
            {
                string loggedData = Guid.NewGuid().ToString();
                Log log = _getNewLog<string>(loggedData);
                Manager.Add(log);
                logs.Add(log);
                Console.WriteLine("Logging Data: " + log.Title);
                for (int j = 1; j <= 5; j++)
                {
                    var newLog = _getNewLog<Guid>(Guid.NewGuid());
                    newLog = log.AddChild(newLog);
                    Console.WriteLine("\tLogging Child for [" + log.ID + "] Data: " + newLog.Title);
                }
            }
        }

        private static Log _getNewLog<T>(T loggedData)
        {
            var log = Log.Create<T>(loggedData);
            log.Title = Guid.NewGuid().ToString();
            log.Description = Guid.NewGuid().ToString();
            log.IpAddress = Util.GetIpAddress();
            return log;
        }
    }
}
