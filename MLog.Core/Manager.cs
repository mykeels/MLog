using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MLog.Core.Extensions;
using Microsoft.Extensions.Configuration;

namespace MLog.Core
{
    public class Manager
    {
        public static Log Add(Log log, Log parent = null)
        {
            if (log.ID > 0) throw new Exception("log.ID should be zero");
            if (String.IsNullOrEmpty(log.Application)) throw new Exception("log.Application should be defined");
            if (String.IsNullOrEmpty(log.Title)) throw new Exception("log.Title should be defined");
            if (parent != null)
            {
                log.Parent = parent.ID;
            }
            string connectionString = Convert.ToString((string)Util.GetAppSettings("appSettings:MongoDBUri"));
            MongoClient _client = new MongoClient(connectionString);
            IMongoCollection<Log> logs = GetLogs(_client);
            log.ID = logs.Count(FilterDefinition<Log>.Empty) + 1;
            logs.InsertOne(log);
            return log;
        }

        public static List<Log> GetLogs(Log log)
        {
            string connectionString = Convert.ToString((string)Util.GetAppSettings("appSettings:MongoDBUri"));
            MongoClient _client = new MongoClient(connectionString);
            IMongoCollection<Log> logs = GetLogs(_client);
            var query = logs.AsQueryable();
            return query.Where(l => (log.ID > 0 && l.ID == log.ID) || (String.IsNullOrEmpty(log.Application) && l.Application.Equals(log.Application)) || 
            (log.Parent > 0 && l.Parent.Equals(log.Parent)) || (String.IsNullOrEmpty(log.Title) && l.Title.Equals(log.Title))).ToList();
        }

        public static IMongoCollection<Log> GetLogs(MongoClient _client, MongoCollectionSettings _settings = null)
        {
            var _db = _client.GetDatabase(Util.GetAppSettings("appSettings:MongoDBName"));
            return _db.GetCollection<Log>("tbl_Logs", _settings);
        }

        public static T AddRequestAndResponse<T, InpuT>(string title, InpuT inputVal, Func<InpuT, T> func)
        {
            Log log = Log.Create<InpuT>(inputVal);
            log.Title = title;
            log.Description = log.Application + " Data Log for " + title;
            log.IpAddress = Util.GetIpAddress();
            log = Manager.Add(log);
            T response = func(inputVal);

            Log responseLog = Log.Create(response);
            responseLog.Title = "Response for Request #" + log.ID + "(" + log.Title + ")";
            responseLog.Description = log.Application + " Data Log for " + responseLog.Title;
            responseLog.IpAddress = log.IpAddress;
            log.AddChild(responseLog);

            return response;
        }
    }
}
