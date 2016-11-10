# <span style="color:#00AAFF">M</span><span style="color:#FFAA00">L</span><span style="color:#DDAA22">o</span><span style="color:gold">g</span> 

Now with .NET Core Support, MLog is a simple .NET Library for Data Logging in your .NET Apps.

## How to Use

#### Simple Usage

```csharp
//Task: Create a Log for the creation of a New Student
Mlog.Log log = Mlog.Log.Create<Student>(newStudent);
log.Title = "New Student Created: [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
Mlog.Manager.Add(log);
```

#### Log Relationships

Logs have parent-children relationships using the `Parent` property of each Log-Item. To Create a Child Log:

```csharp
//Task: Create a Log for the creation of a New Student's Course
var coursesLog = Mlog.Log.Create<List<Course>>(newStudent.GetCourses());
log.Title = "New Courses added for [" + newStudent.FullName + "]";
log.Description = log.Application + " Data Log for " + title;
log.AddChild(coursesLog);
Mlog.Manager.Add(log);
```

#### Log Ip Address

For documentation purposes, you might want to include the IP Address of the User who performs an Action in its Log.

```csharp
log.IpAddress = MLog.Util.GetIpAddress();
```

#### Log Requests and Responses in WebApi programs

```csharp
    [Route("api/students/add")]
    public Response<Student> Add([FromBody()] Models.User val)
    {
        return MLog.Manager.AddRequestAndResponse<Response<Student>, User>("api-student-add", val, (Student values) =>
        {
            return new Response<Student>("New Student Created", Student.Add(values), true);
        });
    }
```

## Dependencies

- Newtonsoft JSON [(view)](http://www.newtonsoft.com/json)
- MongoDB.Core [(view)](https://www.nuget.org/packages/MongoDB.Driver.Core/)
- MongoDB.Driver [(view)](https://www.nuget.org/packages/MongoDB.Driver.Core/)
- Microsoft .NET Framework 4.5.2 [(download)](https://www.microsoft.com/en-us/download/details.aspx?id=42642)

## Supported Databases

- SQL Server [(schema)](MLog.Data)
- MongoDB [(view)](https://mongodb.com)

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

The dream is to make MLog multi-platform using the .NET Standard, and ensure it is possible to use MLog with the following Databases:

- SQL Server
- MongoDB

Also, we need to provide developer-users with a way to configure MLog using Lamda Expressions, so they can make MLog behave in any number of ways to suit their applications.

## Enjoying MLog? How you can thank me ...

Follow me on [twitter](https://twitter.com/mykeels). Star this github repo . Check out [my other projects](https://github.com/mykeels?tab=repositories) and see if you like them.

[Provide useful code critism](https://github.com/mykeels/MLog/issues). I'd love to hear from you, really.

Thanks! Ikechi Michael I.

## License

The MIT License. Please see [License File](LICENSE) for more information.