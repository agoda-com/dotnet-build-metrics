using Agoda.DevFeedback.Common;
using System;
using System.Collections;
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
            const string TAG_PREFIX = "DEVFEEDBACK_TAG_";

            // Get all environment variables and filter for ones starting with DEVFEEDBACK_TAG_
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
            {
                string key = de.Key.ToString();
                if (key.StartsWith(TAG_PREFIX, StringComparison.OrdinalIgnoreCase))
                {
                    // Remove the prefix to get the clean tag name
                    string tagName = key.Substring(TAG_PREFIX.Length).ToLowerInvariant();
                    string tagValue = de.Value?.ToString() ?? string.Empty;

                    if (!string.IsNullOrEmpty(tagValue))
                    {
                        tags.Add(tagName, tagValue);
                    }
                }
            }

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
