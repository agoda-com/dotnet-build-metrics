# Dotnet Build Metrics

MS Build task library compatible with MSBuild and dotnet core CLI.

Designed to collect compile time metrics from developers local workstation and log for later analysis of Time to Dev Feedback.

To use the task simply add the `Agoda.Builds.Metrics` as a dependency to your projects. Then you need to add the following line to your `.csproj` file ...

```
<Import 
  Project="$(MSBuildThisFileDirectory)\..\packages\agoda.builds.metrics\1.0.6\build\Agoda.Builds.Metrics.targets" 
  Condition="Exists('$(MSBuildThisFileDirectory)\..\packages\agoda.builds.metrics\1.0.6\build\Agoda.Builds.Metrics.targets')"
/>
```

Note that the version number is embedded in the path, if you upgrade the library you have to update all of the project files as well.

Currently it only outputs to the command prompt.

Example in Visual Studio using MSBuild.

![](doc/img/VSBuildOutput.PNG)

Exmaple in Dotnet core CLI.

![](doc/img/DotnetCLIBuildTimeOutput.PNG)

