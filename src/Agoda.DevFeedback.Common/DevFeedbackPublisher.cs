using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Agoda.DevFeedback.Common
{
    public static class DevFeedbackPublisher
    {
        private const string BASE_URL = "http://compilation-metrics/";

        public static void PublishBuildData(object data)
        {
            var targetEndpoint = GetApiEndpoint();
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            Send(targetEndpoint, content);
        }

        public static void PublishNUnitTestCase(object data)
        {
            var targetEndpoint = GetNunitApiEndpoint();
            var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");

            Send(targetEndpoint, content);
        }

        private static void Send(string endpoint, HttpContent content)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(2);

                var response = httpClient.PostAsync(endpoint, content).Result;

                response.EnsureSuccessStatusCode();
            }
        }

        static string GetApiEndpoint()
        {
            var apiEndpoint = Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT");

            return string.IsNullOrEmpty(apiEndpoint) ? $"{BASE_URL}dotnet" : apiEndpoint;
        }

        static string GetNunitApiEndpoint()
        {
            var apiEndpoint = Environment.GetEnvironmentVariable("NUNIT_METRICS_ES_ENDPOINT");

            return string.IsNullOrEmpty(apiEndpoint) ? $"{BASE_URL}dotnet/nunit" : apiEndpoint;
        }
    }
}
