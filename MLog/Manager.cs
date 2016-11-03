using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MLog
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
            using (MLogDataContext db = new MLogDataContext())
            {
                db.tblLogs.InsertOnSubmit(log.Map());
                db.SubmitChanges();
            }
            using (MLogDataContext db = new MLogDataContext())
            {
                log.ID = (from pp in db.tblLogs where pp.Title == log.Title orderby pp.ID descending select pp.ID).ToList().FirstOrDefault();
            }
            return log;
        }

        public static List<Log> GetLogs(Log log)
        {
            using (MLogDataContext db = new MLogDataContext())
            {
                if (log == null)
                {
                    return db.tblLogs.Take(500).ToList().Select((tlog) => Log.Map(tlog)).ToList();
                }
                else
                {
                    bool applicationValid = !String.IsNullOrEmpty(log.Application);
                    bool idValid = log.ID > 0;
                    bool parentValid = log.Parent > 0;
                    bool titleValid = !String.IsNullOrEmpty(log.Title);
                    return (from pp in db.tblLogs
                                 where (idValid && pp.ID.Equals(log.ID)) || (applicationValid && pp.Application.Equals(log.Application)) ||
           (parentValid && pp.Parent.Equals(log.Parent)) || (titleValid) && pp.Title.ToLower().Equals(log.Title)
                                 select pp).ToList().Select((tlog) => Log.Map(tlog)).ToList();
                }
            }
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
