using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;
using RestSharp;

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
                var currentPath = Environment.CurrentDirectory;
                while (!RepositoryInformation.isValidPath(currentPath))
                {
                    currentPath = Path.GetFullPath(Path.Combine(currentPath, @"../"));
                }
                var repositoryInformation = RepositoryInformation.GetRepositoryInformation(currentPath);
                var data = new
                {
                    id = Guid.NewGuid(),
                    userName = Environment.UserName,
                    cpuCount = Environment.ProcessorCount,
                    hostname = Environment.MachineName,
                    platform = Environment.OSVersion.Platform,
                    os = Environment.OSVersion.VersionString,
                    timeTaken = DebugOutput,
                    branch = repositoryInformation.BranchName,
                    type = ".Net",
                    repository = repositoryInformation.Origin,
                    date = DateTime.UtcNow
                };
                var client = new RestClient("http://backend-elasticsearch.agoda.local:9200/build-metrics/_doc/?pretty");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");

                request.AddParameter("application/json", JsonConvert.SerializeObject(data), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                Console.WriteLine(response.Content);

            }
            catch (Exception ex)
            {
                Log.LogError(ex.Message);
            }

            return true;
        }
    }
}
