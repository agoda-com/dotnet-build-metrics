using System;
using System.Collections.Generic;
using System.Linq;
using Agoda.DevFeedback.Common;

namespace Agoda.Tests.Metrics
{
    public class TestCasePayload
    {
        public TestCasePayload(string metricsVersion, GitContext gitContext, IList<TestCase> testCases)
        {
            Id = Guid.NewGuid().ToString();
            UserName = Environment.UserName;
            
            if(!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("GITLAB_USER_LOGIN")))
                UserName = Environment.GetEnvironmentVariable("GITLAB_USER_LOGIN");

            CpuCount = Environment.ProcessorCount;
#pragma warning disable AG0035
            Hostname = Environment.MachineName;
#pragma warning restore AG0035
            Platform = (int)Environment.OSVersion.Platform;
            Os = Environment.OSVersion.VersionString;
            Repository = gitContext.RepositoryUrl;
            RepositoryName = gitContext.RepositoryName;
            Branch = gitContext.BranchName;
            NUnitTestCases = testCases.ToList();
            IsDebuggerAttached = System.Diagnostics.Debugger.IsAttached;
            var ciJobId = Environment.GetEnvironmentVariable("CI_JOB_ID");
            if (!string.IsNullOrEmpty(ciJobId))
            {
                RunId = ciJobId;
            }
            else
            {
                RunId = Guid.NewGuid().ToString();
            }
        }

        public bool IsDebuggerAttached { get; set; }

        public string Id { get; set; }

        public string UserName { get; set; }

        public int CpuCount { get; set; }

        public string Hostname { get; set; }

        public int Platform { get; set; }

        public string Os { get; set; }

        public string Branch { get; set; }

        public string Repository { get; set; }

        public string RepositoryName { get; set; }

        public IList<TestCase> NUnitTestCases { get; set; }

        public string RunId { get; set; }

    }
}
