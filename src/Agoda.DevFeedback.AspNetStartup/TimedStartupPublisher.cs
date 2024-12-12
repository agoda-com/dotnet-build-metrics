using Agoda.DevFeedback.Common;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Agoda.DevFeedback.AspNetStartup
{
    internal static class TimedStartupPublisher
    {
        public static void Publish(string type, TimeSpan timeTaken)
        {
            var gitContext = GitContextReader.GetGitContext();
            
            var tags = new Dictionary<string, string>();

            if(Environment.GetEnvironmentVariable("DISABLE_QA") != null) tags.Add("isDisableQa", Environment.GetEnvironmentVariable("DISABLE_QA"));

            var result = new DevFeedbackData(
                metricsVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                type: type,
                projectName: Assembly.GetEntryAssembly()?.GetName().Name,
                timeTaken: timeTaken.TotalMilliseconds.ToString(),
                gitContext: gitContext,
                tags: tags
            );

            DevFeedbackPublisher.Publish(apiEndpoint: null, result, DevLocalDataType.Build);
        }
    }
}
