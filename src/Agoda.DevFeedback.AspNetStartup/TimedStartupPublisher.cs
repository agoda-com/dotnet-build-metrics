using Agoda.DevFeedback.Common;
using System.Reflection;

namespace Agoda.DevFeedback.AspNetStartup
{
    internal static class TimedStartupPublisher
    {
        public static void Publish(TimeSpan startupTime)
        {
            var gitContext = GitContextReader.GetGitContext();

            var result = new DevFeedbackData(
                metricsVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                type: ".AspNetStartup",
                projectName: Assembly.GetEntryAssembly()?.GetName().Name,
                timeTaken: startupTime.TotalMilliseconds.ToString(),
                gitContext: gitContext
            );

            DevFeedbackPublisher.Publish(apiEndpoint: null, result);
        }
    }
}
