using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Agoda.DevFeedback.Common
{
    public static class DevFeedbackPublisher
    {
        public static void Publish<T>(string apiEndpoint, T data)
        {
            Publish(apiEndpoint,data, DevLocalDataType.Build);
        }

        public static void Publish<T>(string apiEndpoint, T data, DevLocalDataType devLocalDataType)
        {
            var targetEndpoint = string.Empty;
            switch (devLocalDataType)
            {
                case DevLocalDataType.Build:
                    targetEndpoint = GetApiEndpoint(apiEndpoint);
                    break;
                case DevLocalDataType.NUint:
                    targetEndpoint = GetNunitApiEndpoint(apiEndpoint);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(devLocalDataType), devLocalDataType, null);
            }
            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromSeconds(2);

                var content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync(targetEndpoint, content).Result;

                response.EnsureSuccessStatusCode();
            }
        }

        private const string BASE_URL = "http://compilation-metrics/";
        //private const string BASE_URL = "http://localhost:5000/";
        static string GetApiEndpoint(string apiEndpoint)
        {
            if (string.IsNullOrEmpty(apiEndpoint))
            {
                apiEndpoint = Environment.GetEnvironmentVariable("BUILD_METRICS_ES_ENDPOINT");
            }

            return string.IsNullOrEmpty(apiEndpoint) ? $"{BASE_URL}dotnet" : apiEndpoint;
        }

        static string GetNunitApiEndpoint(string apiEndpoint)
        {
            if (string.IsNullOrEmpty(apiEndpoint))
            {
                apiEndpoint = Environment.GetEnvironmentVariable("NUNIT_METRICS_ES_ENDPOINT");
            }

            return string.IsNullOrEmpty(apiEndpoint) ? $"{BASE_URL}dotnet/nunit" : apiEndpoint;
        }
    }
}
