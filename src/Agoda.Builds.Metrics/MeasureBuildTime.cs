using Agoda.DevFeedback.Common;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;

namespace Agoda.Builds.Metrics
{
    public class MeasureBuildTime : Task
    {
        /// <summary>
        /// Set by the 'CaptureBuildTime' build event.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Set by the 'CaptureBuildTime' build event.
        /// </summary>
        public string StartDateTime { get; set; }

        /// <summary>
        /// Set by the 'CaptureBuildTime' build event.
        /// </summary>
        public string EndDateTime { get; set; }

        /// <summary>
        /// Set by the Task.Execute method.
        /// Used by the 'CaptureBuildTime' build event.
        /// </summary>
        [Output]
        public string BuildTimeMilliseconds { get; set; }

        public override bool Execute()
        {
            BuildTimeMilliseconds = DateTime.Parse(EndDateTime).Subtract(DateTime.Parse(StartDateTime)).TotalMilliseconds.ToString();

            try
            {
                var gitContext = GitContextReader.GetGitContext();

                var data = new DevFeedbackData(
                    metricsVersion: typeof(MeasureBuildTime).Assembly.GetName().Version.ToString(),
                    type: ".Net",
                    projectName: ProjectName,
                    timeTaken: BuildTimeMilliseconds,
                    gitContext: gitContext
                );

                DevFeedbackPublisher.PublishBuildData(data);
            }
            catch (GitContextException ex)
            {
                Log.LogMessage($"The build time will not be published: {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.LogMessage("An error occured while capturing the build time: " + ex);
            }

            return true;
        }
    }
}
