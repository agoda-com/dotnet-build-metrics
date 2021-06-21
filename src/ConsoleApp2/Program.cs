using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromMilliseconds(100);
                httpClient.BaseAddress = new Uri("http://localhost:3000");
                var data = new
                {
                    username = Environment.UserName,
                    cpuCount = Environment.ProcessorCount,
                    hostname = Environment.MachineName,
                    platform = Environment.OSVersion.Platform,
                    os = Environment.OSVersion.VersionString,
                    timeTaken = 3,
                    String.Empty
                };
                var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                var response = httpClient.PostAsync("/metrics", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine(response.ReasonPhrase);
                }
            }
        }
    }
}
