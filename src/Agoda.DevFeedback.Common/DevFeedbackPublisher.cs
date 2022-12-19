using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Agoda.DevFeedback.Common
{
    public static class DevFeedbackPublisher
    {
        public static void Publish(string apiEndpoint, DevFeedbackData data)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(GetApiEndpoint(apiEndpoint), content).Result;

                response.EnsureSuccessStatusCode();
            }
        }

        static string GetApiEndpoint(string apiEndpoint)
        {
            if (string.IsNullOrEmpty(apiEndpoint))
            {
                apiEndpoint = Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT");
            }

            return string.IsNullOrEmpty(apiEndpoint) ? "http://compilation-metrics/dotnet" : apiEndpoint;
        }
    }
}
