using System;
using System.Text.Json.Serialization;

namespace Agoda.DevFeedback.Common
{
    public class DevFeedbackData
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        
        [JsonPropertyName("metricsVersion")]
        public string MetricsVersion { get; set; }
        
        [JsonPropertyName("userName")]
        public string UserName { get; set; }
        
        [JsonPropertyName("cpuCount")]
        public int CpuCount { get; set; }
        
        [JsonPropertyName("hostname")]
        public string Hostname { get; set; }
        
        [JsonPropertyName("platform")]
        public PlatformID Platform { get; set; }
        
        [JsonPropertyName("os")]
        public string Os { get; set; }
        
        [JsonPropertyName("timeTaken")]
        public string TimeTaken { get; set; }
        
        [JsonPropertyName("branch")]
        public string Branch { get; set; }
        
        [JsonPropertyName("type")]
        public string Type { get; set; }
        
        [JsonPropertyName("projectName")]
        public string ProjectName { get; set; }
        
        [JsonPropertyName("repository")]
        public string Repository { get; set; }
        
        [JsonPropertyName("repositoryName")]
        public string RepositoryName { get; set; }
        
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        public DevFeedbackData(
            string metricsVersion,
            string type,
            string projectName,
            string timeTaken,
            GitContext gitContext
        )
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
        }
    }
}
