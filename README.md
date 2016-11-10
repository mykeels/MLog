# MLog 

Now with .NET Core Support, MLog is a simple .NET Library for Data Logging in your .NET Apps.

## How to Use

#### Simple Usage

```cs
//Task: Create a Log for the creation of a New Student
Mlog.Log log = Mlog.Log.Create<Student>(newStudent);
log.Title = "New Student Created: [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
Mlog.Manager.Add(log);
```

#### Log Relationships

Logs have parent-children relationships using the `Parent` property of each Log-Item. To Create a Child Log:

```cs
//Task: Create a Log for the creation of a New Student's Course
var coursesLog = Mlog.Log.Create<List<Course>>(newStudent.GetCourses());
log.Title = "New Courses added for [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
log.AddChild(coursesLog);
Mlog.Manager.Add(log);
```

#### Log Ip Address

For documentation purposes, you might want to include the IP Address of the User who performs an Action in its Log.

```cs
log.IpAddress = MLog.Util.GetIpAddress();
```

#### Log Requests and Responses in WebApi programs

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

## Supported Databases

- SQL Server [(View Schema)](MLog.Data)
- MongoDB

## Setup Web.Config

#### Set the [Application] Field

```xml
  <appSettings>
    <add key="mlog-app" value="MlogTests" />
  </appSettings>
```

#### Database Connection String

```xml
  <connectionStrings>
    <add name="MLog.Properties.Settings.MLogConnectionString" connectionString="Data Source=MYKEELS-PC;Initial Catalog=MLog;User ID=[your db username];Password=[your password]"
        providerName="System.Data.SqlClient" />
  </connectionStrings>
```

## Setup config.json

```json
{
  "appSettings": {
    "mlog-app": "MlogTests",
    "MongoDBUri": "mongodb://localhost:27017",
    "MongoDBName": "MLogTests"
  },
  "connectionStrings": [
    {
      "name": "MLog.Properties.Settings.MLogConnectionString",
      "connectionString": "Data Source=MYKEELS-PC;Initial Catalog=MLog;User ID=mykeels",
      "providerName": "System.Data.SqlClient"
    }
  ]
}
```

## Future Improvements

Hmmm, I'm not sure how to improve MLog right now; However, as I use it, I'll figure out more ways it can be improved.

## Enjoying MLog? How you can thank me ...

Follow me on [twitter](https://twitter.com/mykeels). <!-- Place this tag where you want the button to render. -->
<a class="github-button" href="https://github.com/mykeels/MLog" data-icon="octicon-star" data-style="mega" data-count-href="/mykeels/MLog/stargazers" data-count-api="/repos/mykeels/MLog#stargazers_count" data-count-aria-label="# stargazers on GitHub" aria-label="Star mykeels/MLog on GitHub">Star</a> this github repo . Check out [my other projects](https://github.com/mykeels?tab=repositories) and see if you like them.

[Provide useful code critism](https://github.com/mykeels/MLog/issues). I'd love to hear from you, really.

Thanks! Ikechi Michael I.

## License

The MIT License. Please see [License File](LICENSE) for more information.