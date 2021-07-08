# Dotnet Build Metrics

This is a custom MSBuild task that will measure the time to build each project and publishes the information to an ElasticSearch datastore. The intent is to measure and improve developer experience on local workstations.

**_Username and hostname are collected as a part of build information_**

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

To use the task simply install the [`Agoda.Builds.Metrics` NuGet package](https://www.nuget.org/packages/Agoda.Builds.Metrics) to all of the projects in your solution, this will automatically enable metrics publication for that project.

ElasticSearch URL and the name of the index are taken from environment variables if they are present as per the following table:

|Parameter              |Environment Variable     |Default Value                    |
|-----------------------|-------------------------|---------------------------------|
|Elastic Search Endpoint|BUILD_METRICS_ES_ENDPOINT|http://backend-elasticsearch:9200|
|Elastic Search Index   |BUILD_METRICS_ES_INDEX   |build-metrics                    |

Example build log output in Visual Studio using MSBuild.

![](doc/img/VSBuildOutput.PNG)

Example build log output in Dotnet core CLI.

![](doc/img/DotnetCLIBuildTimeOutput.PNG)
