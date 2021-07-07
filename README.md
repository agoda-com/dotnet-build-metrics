# Dotnet Build Metrics

MS Build task library compatible with MSBuild and dotnet core CLI.

Designed to collect compile time metrics from developers local workstation and log for later analysis of Time to Dev Feedback.  
**_It collects username and hostname as a part of build information_**

```
{
                    "id": "026fe42b-7888-4a76-81ee-2ab431ae4987",
                    "userName": "aanand",
                    "cpuCount": 12,
                    "hostname": "25BVM13",
                    "platform": 2,
                    "os": "Microsoft Windows NT 10.0.19042.0",
                    "timeTaken": "453",
                    "branch": "master",
                    "type": ".Net",
                    "projectName": "Agoda.Cronos.Accommodation",
                    "repository": "git@github.xxxx.io:front-end/dotnet-build-metrics.git",
                    "date": "2021-06-28T02:53:22.1420552Z"
 }
```

To use the task simply add the `Agoda.Builds.Metrics` NuGet package to all of the projects in your solution, this will automatically enable metrics publication for that project.

ElasticSearch URL and the name of the index are taken from environment variables if they are present. The fallback values are http://backend-elasticsearch:9200 for the URL and build-metrics for the index. The index must already exist.
```
string uriString = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("url")) ? Environment.GetEnvironmentVariable("url") : "http://backend-elasticsearch:9200";
string index = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("index")) ? Environment.GetEnvironmentVariable("index") : "build-metrics";
 ```
Note that the version number is embedded in the path, if you upgrade the library you have to update all of the project files as well.

Example in Visual Studio using MSBuild.

![](doc/img/VSBuildOutput.PNG)

Exmaple in Dotnet core CLI.

![](doc/img/DotnetCLIBuildTimeOutput.PNG)
