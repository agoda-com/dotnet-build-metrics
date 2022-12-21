# Dotnet Build Metrics

This is a custom MSBuild task that will measure the time to build each project and publishes the information to a datastore. The intent is to measure and improve developer experience on local workstations.

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

Web API URL and the name of the endpoint are taken from environment variables if they are present as per the following table:

|Parameter              |Environment Variable     |Default Value                        |
|-----------------------|-------------------------|-------------------------------------|
|Elastic Search Endpoint|BUILD_METRICS_ES_ENDPOINT|http://compilation-metrics/dotnet    |

We recommend create a CNAME on your internal DNS servers for compilation-metrics to point at an internal API that captures the data and stores it. 

Example build log output in Visual Studio using MSBuild.

![](doc/img/VSBuildOutput.PNG)

Example build log output in Dotnet core CLI.

![](doc/img/DotnetCLIBuildTimeOutput.PNG)


### Agoda.DevFeedback.AspNetStartup NuGet package

The goal of this package is to measure the application startup time as an additional metrics that affects developer feedback besides build time.
Monoliths are especially prone to slow startup times on developer laptops and this provides visibility and tracking of improvements/regressions thereof.

This package measures the ASP.NET application's startup time in two ways.
1) The time from WebHostBuilder ConfigureServices until HostApplicationLifetime OnStarted sent with type '.AspNetStartup'.
2) The time from WebHostBuilder ConfigureServices until the first HTTP Reponse (measured from middleware) sent with type '.AspNetResponse'.

Two environment variables are required to be set to enable the measurements.
1) `ASPNETCORE_ENVIRONMENT` needs to be set as `Development` for the functionality to be added. This is so it's only enabled locally and not on production.
2) `ASPNETCORE_HOSTINGSTARTUPASSEMBLIES` needs to be/include `Agoda.DevFeedback.AspNetStartup`. This is for package activation due to the use of [HostingStartup](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.hosting.hostingstartupattribute?view=aspnetcore-6.0).

For convenience these environment variables can be set under the lauch profile of the web application's `launchSettings.json` file and committed to the repository.
