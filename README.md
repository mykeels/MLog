# MLog

A Simple .NET Library for Data Logging.

### Simple Usage

```cs
//Task: Create a Log for the creation of a New Student
Mlog.Log log = Mlog.Log.Create<Student>(newStudent);
log.Title = "New Student Created: [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
log.IpAddress = Mlog.Util.GetIpAddress();
Mlog.Manager.Add(log);
```