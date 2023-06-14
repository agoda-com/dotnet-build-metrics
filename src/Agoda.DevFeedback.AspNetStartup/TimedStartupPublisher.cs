using Agoda.DevFeedback.Common;
using System;
using System.Reflection;

namespace Agoda.DevFeedback.AspNetStartup
{
    internal static class TimedStartupPublisher
    {
        public static void Publish(string type, TimeSpan timeTaken)
        {
            var gitContext = GitContextReader.GetGitContext();

            var result = new DevFeedbackData(
                metricsVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                type: type,
                projectName: Assembly.GetEntryAssembly()?.GetName().Name,
                timeTaken: timeTaken.TotalMilliseconds.ToString(),
                gitContext: gitContext
            );

            DevFeedbackPublisher.PublishBuildData(result);
        }
    }
}
