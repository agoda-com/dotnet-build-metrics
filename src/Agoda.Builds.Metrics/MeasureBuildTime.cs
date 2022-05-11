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
        public string ApiEndPoint { get; set; }
        public string ProjectName { get; set; }
        [Output]
        public string DebugOutput { get; set; }

        public override bool Execute()
        {
            DebugOutput = (DateTime.Parse(EndDateTime) - DateTime.Parse(StartDateTime)).TotalMilliseconds.ToString();
            try
            {
                var gitRootDir = GetGitDetails("rev-parse --show-toplevel");
                var gitUrl = GetGitDetails("config --get remote.origin.url", gitRootDir);
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
                    branch = GetGitDetails("rev-parse --abbrev-ref HEAD", gitRootDir),
                    type = ".Net",
                    projectName = ProjectName,
                    repository = gitUrl,
                    repositoryName = extractRepositoryName(gitUrl),
                    date = DateTime.UtcNow
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(1);
                    PopulateBuildMetricESDetails();
                    var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                    var responses = httpClient.PostAsync(ApiEndPoint, content).Result;
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

        private static string extractRepositoryName(string gitUrl)
        {
            var repositoryName = gitUrl.Substring(gitUrl.LastIndexOf("/") + 1);
            return repositoryName.Contains(".git") ? repositoryName.Substring(0, repositoryName.LastIndexOf(".")) : repositoryName;
        }

        private void PopulateBuildMetricESDetails()
        {
            if (string.IsNullOrEmpty(ApiEndPoint))
            {
                ApiEndPoint = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT")) ? Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT") : "http://compilation-metrics/dotnet";
            }
        }

        private static string GetGitDetails(string arg, string cwd = null)
        {
            string executableName = "git";
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                executableName += ".exe";
            ProcessStartInfo startInfo = new ProcessStartInfo(executableName);

            if (cwd != null)
            {
                startInfo.WorkingDirectory = cwd;
            }

            startInfo.UseShellExecute = false;
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
