using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Agoda.DevFeedback.Common
{
    public class DevFeedbackData
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("metricsVersion")]
        public string MetricsVersion { get; set; }
        
        [JsonProperty("userName")]
        public string UserName { get; set; }
        
        [JsonProperty("cpuCount")]
        public int CpuCount { get; set; }
        
        [JsonProperty("hostname")]
        public string Hostname { get; set; }
        
        [JsonProperty("platform")]
        public PlatformID Platform { get; set; }
        
        [JsonProperty("os")]
        public string Os { get; set; }
        
        [JsonProperty("timeTaken")]
        public string TimeTaken { get; set; }
        
        [JsonProperty("branch")]
        public string Branch { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }
        
        [JsonProperty("projectName")]
        public string ProjectName { get; set; }
        
        [JsonProperty("repository")]
        public string Repository { get; set; }
        
        [JsonProperty("repositoryName")]
        public string RepositoryName { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("tags")]
        public Dictionary<string,string> Tags { get; set; }

        [JsonProperty("isDebuggerAttached")]
        public bool IsDebuggerAttached { get; set; }

        public DevFeedbackData(
            string metricsVersion,
            string type,
            string projectName,
            string timeTaken,
            GitContext gitContext,
            Dictionary<string,string> tags = null)
        {
            Id = Guid.NewGuid();
            Type = type;
            MetricsVersion = metricsVersion;
            UserName = Environment.UserName;
            CpuCount = Environment.ProcessorCount;
            Hostname = Environment.MachineName;
            Platform = Environment.OSVersion.Platform;
            Os = Environment.OSVersion.VersionString;
            ProjectName = projectName;
            TimeTaken = timeTaken;
            Repository = gitContext.RepositoryUrl;
            RepositoryName = gitContext.RepositoryName;
            Branch = gitContext.BranchName;
            Date = DateTime.UtcNow;
            Tags = tags;
            IsDebuggerAttached = System.Diagnostics.Debugger.IsAttached;
        }

    }
}
