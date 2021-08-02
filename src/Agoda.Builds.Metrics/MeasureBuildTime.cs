using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

using System.Text.Json;
namespace Agoda.Builds.Metrics
{
    public class MeasureBuildTime : Task
    {
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public string ElasticEndPoint { get; set; }
        public string ElasticIndex { get; set; }
        public string ProjectName { get; set; }
        [Output]
        public string DebugOutput { get; set; }

        public override bool Execute()
        {
            DebugOutput = (DateTime.Parse(EndDateTime) - DateTime.Parse(StartDateTime)).TotalMilliseconds.ToString();
            try
            {
                var gitUrl = GetGitDetails("config --get remote.origin.url");
                var data = new
                {
                    id = Guid.NewGuid(),
                    metricsVersion = typeof(MeasureBuildTime).Assembly.GetName().Version.ToString(),
                    userName = Environment.UserName,
                    cpuCount = Environment.ProcessorCount,
                    hostname = Environment.MachineName,
                    platform = Environment.OSVersion.Platform,
                    os = Environment.OSVersion.VersionString,
                    timeTaken = DebugOutput,
                    branch = GetGitDetails("rev-parse --abbrev-ref HEAD"),
                    type = ".Net",
                    projectName = ProjectName,
                    repository = gitUrl,
                    repositoryName = gitUrl.Substring(gitUrl.LastIndexOf("/") + 1).Split('.')[0],
                    date = DateTime.UtcNow
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(1);
                    PopulateBuildMetricESDetails();
                    httpClient.BaseAddress = new Uri(ElasticEndPoint);
                    var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                    var responses = httpClient.PostAsync($"/{ElasticIndex}/_doc", content).Result;
                    if (!responses.IsSuccessStatusCode)
                    {
                        Log.LogMessage($"Unable to publish metrics - {responses.ReasonPhrase}");
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogMessage($"Unexpected issue while generating metrics - {ex.Message}");
            }

            return true;
        }

        private void PopulateBuildMetricESDetails()
        {
            if (string.IsNullOrEmpty(ElasticEndPoint))
            {
                ElasticEndPoint = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT")) ? Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT") : "http://backend-elasticsearch:9200";
            }
            if (string.IsNullOrEmpty(ElasticIndex))
            {
                ElasticIndex = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_METRICS_ES_INDEX")) ? Environment.GetEnvironmentVariable("BUILD_METRICS_ES_INDEX") : "build-metrics";
            }
        }

        private static string GetGitDetails(string arg)
        {
            string executableName = "git";
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                executableName += ".exe";
            ProcessStartInfo startInfo = new ProcessStartInfo(executableName);

            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = arg;

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            var gitBranch = process.StandardOutput.ReadLine();
            return gitBranch;
        }
    }
}
