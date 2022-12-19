using Agoda.DevFeedback.Common;
using System.Reflection;

namespace Agoda.DevFeedback.AspNetStartup
{
    internal static class TimedStartupPublisher
    {
        public static void Publish(TimeSpan startupTime)
        {
            var gitContext = GitContextReader.GetGitContext();

            var result = new BuildTimeData(
                metricsVersion: Assembly.GetExecutingAssembly().GetName().Version?.ToString(),
                type: ".AspNetStartup",
                projectName: Assembly.GetEntryAssembly()?.GetName().Name,
                timeTaken: startupTime.TotalMilliseconds.ToString(),
                gitContext: gitContext
            );

            BuildTimePublisher.Publish(apiEndpoint: null, result);
        }
    }
}
