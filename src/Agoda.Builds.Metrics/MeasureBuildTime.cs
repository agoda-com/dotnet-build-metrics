using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Linq;
using LibGit2Sharp;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Net.Http;
using Newtonsoft.Json;

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
                var username = Environment.UserName;
                var cpuCount = Environment.ProcessorCount;
                var hostname = Environment.MachineName;
                var platform = Environment.OSVersion.Platform;
                var os = Environment.OSVersion.VersionString;
                var gitBranch = string.Empty;
                using (var repo = new Repository(Environment.CurrentDirectory))
                {
                    gitBranch = repo.Branches.Where(b => !b.IsRemote && b.IsCurrentRepositoryHead).FirstOrDefault().FriendlyName;
                }

                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromMilliseconds(100);
                    httpClient.BaseAddress = new Uri("http://build-mertics");
                    var data = new
                    {
                        username = Environment.UserName,
                        cpuCount = Environment.ProcessorCount,
                        hostname = Environment.MachineName,
                        platform = Environment.OSVersion.Platform,
                        os = Environment.OSVersion.VersionString,
                        timeTaken = DebugOutput,
                        gitBranch
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync("/api/buildmetric/track", content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        Log.LogError(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }

            return true;
        }
    }
}
