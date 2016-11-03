# MLog

A Simple .NET Library for Data Logging.

### Simple Usage

```cs
//Task: Create a Log for the creation of a New Student
Mlog.Log log = Mlog.Log.Create<Student>(newStudent);
log.Title = "New Student Created: [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
Mlog.Manager.Add(log);
```

### Log Relationships

Logs have parent-children relationships using the `Parent` property of each Log-Item. To Create a Child Log:

```cs
//Task: Create a Log for the creation of a New Student's Course
var coursesLog = Mlog.Log.Create<List<Course>>(newStudent.GetCourses());
log.Title = "New Courses added for [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
log.AddChild(coursesLog);
Mlog.Manager.Add(log);
```

### Log Ip Address

For documentation purposes, you might want to include the IP Address of the User who performs an Action in its Log.

```cs
log.IpAddress = MLog.Util.GetIpAddress();
```

### Set the [Application] Field via the Web.Config

```xml
  <appSettings>
    <add key="mlog-app" value="MlogTests" />
  </appSettings>
```

### Log Requests and Responses in WebApi programs

```cs
    [Route("api/students/add")]
    public Response<Student> Add([FromBody()] Models.User val)
    {
        return MLog.Manager.AddRequestAndResponse<Response<Student>, User>("api-student-add", val, (Student values) =>
        {
            return new Response<Student>("New Student Created", Student.Add(values), true);
        });
    }
```