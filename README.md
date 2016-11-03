# MLog

A Simple .NET Library for Data Logging.

```csharp
Mlog.Log log = Mlog.Log.Create<Student>(newStudent);
log.Title = title;
log.Description = log.Application + " Data Log for " + title;
log.IpAddress = Mlog.Util.GetIpAddress();
Mlog.Manager.Add(log);
```