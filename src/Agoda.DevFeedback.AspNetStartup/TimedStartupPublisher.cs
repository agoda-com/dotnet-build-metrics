using Agoda.DevFeedback.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Agoda.DevFeedback.AspNetStartup
{
    internal static class TimedStartupPublisher
    {
        public static void Publish(string type, TimeSpan timeTaken)
        {
            const string TagPrefix = "DEVFEEDBACK_TAG_";

            var gitContext = GitContextReader.GetGitContext();

            // Use LINQ and dictionary comprehension for cleaner tag collection
            var tags = Environment.GetEnvironmentVariables()
                .Cast<DictionaryEntry>()
                .Select(entry => (Key: entry.Key.ToString()!, Value: entry.Value?.ToString()))
                .Where(entry => entry.Key.StartsWith(TagPrefix, StringComparison.OrdinalIgnoreCase)
                                && !string.IsNullOrEmpty(entry.Value))
                .ToDictionary(
                    entry => entry.Key[TagPrefix.Length..].ToLowerInvariant(),
                    entry => entry.Value!
                );

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
