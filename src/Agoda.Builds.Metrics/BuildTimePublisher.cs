using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Agoda.Builds.Metrics
{
    internal static class BuildTimePublisher
    {
        public static void Publish(string apiEndpoint, BuildTimeData result)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                var content = new StringContent(JsonSerializer.Serialize(result), Encoding.UTF8, "application/json");
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
