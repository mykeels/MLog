using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MLog;
using Extensions;

namespace MLog.Tests
{
    public class Program
    {
        public static void Main(string[] args)
        {
            _createLogsAndRelationships();
            Console.WriteLine();
            Console.Read();
        }

        private static void _createLogsAndRelationships()
        {
            var logs = new List<Log>();
            for (int i = 1; i <= 5; i++)
            {
                string loggedData = StringX.RandomLetters(50);
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
            log.Title = StringX.RandomLetters(50);
            log.Description = StringX.RandomLetters(500);
            log.IpAddress = Convert.ToInt32(Number.Rnd() * 255) + "." + Convert.ToInt32(Number.Rnd() * 255) + "." + Convert.ToInt32(Number.Rnd() * 255) + "." + Convert.ToInt32(Number.Rnd() * 255);
            return log;
        }
    }
}
