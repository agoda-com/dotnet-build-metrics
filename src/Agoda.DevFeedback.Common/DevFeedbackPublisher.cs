using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Agoda.DevFeedback.Common
{
    public static class DevFeedbackPublisher
    {
        /// <summary>
        /// Data data type to endpoints
        /// </summary>
        private static Dictionary<DevLocalDataType, List<string>> _dataTypeEndpoints = new Dictionary<DevLocalDataType, List<string>>()
        {
            { DevLocalDataType.Build, new List<string>() { "dotnet", "BUILD_METRICS_ES_ENDPOINT" } },
            { DevLocalDataType.NUnit, new List<string>() { "dotnet/nunit", "NUNIT_METRICS_ES_ENDPOINT" } }
        };

        // Default URL
        private const string BASE_URL = "http://compilation-metrics/";

        private const string TEST_TOKEN_ENV_VAR = "DEVFEEDBACK_TEST_TOKEN";
        private const string TEST_TOKEN_HEADER = "X-DevFeedback-Token";

        /// <summary>
        /// Get the endpoint to use with a specific data source.
        /// </summary>
        private static string GetApiEndpoint(DevLocalDataType dataType, string apiEndpoint)
        {
            _dataTypeEndpoints.TryGetValue(dataType, out var endpointInfo);
            if (endpointInfo == null)
                throw new ArgumentOutOfRangeException(nameof(dataType), dataType, null);
            // Fetch from environment if not manually provided
            if (string.IsNullOrEmpty(apiEndpoint))
            {
                apiEndpoint = Environment.GetEnvironmentVariable(endpointInfo[1]);
            }
            return string.IsNullOrEmpty(apiEndpoint) ? $"{BASE_URL}{endpointInfo[0]}" : apiEndpoint;
        }

        /// <summary>
        /// Publish the data as JSON to the appropriate endpoint
        /// </summary>
        public static async Task PublishAsync(string apiEndpoint, object data, DevLocalDataType devLocalDataType)
        {
            var targetEndpoint = GetApiEndpoint(devLocalDataType, apiEndpoint);
            using (var httpClient = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Post, targetEndpoint))
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                AddTestTokenHeader(request, devLocalDataType);
                var response = await httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();
            }
        }

        /// <summary>
        /// Test reports carry the token from DEVFEEDBACK_TEST_TOKEN, when it is set, so a proxy can let them in.
        /// Build metrics never read or send it.
        /// </summary>
        private static void AddTestTokenHeader(HttpRequestMessage request, DevLocalDataType devLocalDataType)
        {
            if (devLocalDataType != DevLocalDataType.NUnit)
                return;
            var token = Environment.GetEnvironmentVariable(TEST_TOKEN_ENV_VAR);
            if (string.IsNullOrWhiteSpace(token))
                return;
            request.Headers.TryAddWithoutValidation(TEST_TOKEN_HEADER, token);
        }

        /// <summary>
        /// Non-async version of publish
        /// </summary>
        public static void Publish(string apiEndpoint, object data, DevLocalDataType devLocalDataType)
        {
            PublishAsync(apiEndpoint, data, devLocalDataType).Wait();
        }
    }
}
