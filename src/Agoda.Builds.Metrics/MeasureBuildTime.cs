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
        [Output]
        public string DebugOutput { get; set; }
        public override bool Execute()
        {
            DebugOutput = (DateTime.Parse(EndDateTime) - DateTime.Parse(StartDateTime)).TotalMilliseconds.ToString();
            try
            {
                var data = new
                {
                    id = Guid.NewGuid(),
                    userName = Environment.UserName,
                    cpuCount = Environment.ProcessorCount,
                    hostname = Environment.MachineName,
                    platform = Environment.OSVersion.Platform,
                    os = Environment.OSVersion.VersionString,
                    timeTaken = DebugOutput,
                    branch = GetGitDetails("rev-parse --abbrev-ref HEAD"),
                    type = ".Net",
                    repository = GetGitDetails("config --get remote.origin.url"),
                    date = DateTime.UtcNow
                };

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMinutes(1);
                    string uriString = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("url")) ? Environment.GetEnvironmentVariable("url") : "http://backend-elasticsearch:9200";
                    string index = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("index")) ? Environment.GetEnvironmentVariable("index") : "build-metrics";
                    httpClient.BaseAddress = new Uri(uriString);
                    var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                    var responses = httpClient.PostAsync($"/{index}/_doc", content).Result;
                    if (!responses.IsSuccessStatusCode)
                    {
                       Log.LogError(responses.ReasonPhrase);
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }

            return true;
        }

        private static string GetGitDetails(string arg)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("git.exe");

            startInfo.UseShellExecute = false;
            startInfo.WorkingDirectory = Environment.CurrentDirectory;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            startInfo.Arguments = "rev-parse --abbrev-ref HEAD";

            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();

            var gitBranch = process.StandardOutput.ReadLine();
            return gitBranch;
        }
    }
}
