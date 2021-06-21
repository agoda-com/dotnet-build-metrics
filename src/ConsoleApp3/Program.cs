using LibGit2Sharp;
using Newtonsoft.Json;
using NLog.Fluent;
using Serilog;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using Log = Serilog.Log;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            //    using (var httpClient = new HttpClient())
            //    {
            //        httpClient.Timeout = TimeSpan.FromMilliseconds(10000);
            //        httpClient.BaseAddress = new Uri("http://localhost:3000");
            //        var data = new
            //        {
            //            username = Environment.UserName,
            //            cpuCount = Environment.ProcessorCount,
            //            hostname = Environment.MachineName,
            //            platform = Environment.OSVersion.Platform,
            //            os = Environment.OSVersion.VersionString,
            //            timeTaken = 3,
            //            String.Empty
            //        };
            //        var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            //        var response = httpClient.GetAsync("/metrics").Result;
            //        if (!response.IsSuccessStatusCode)
            //        {
            //            Console.WriteLine(response.ReasonPhrase);
            //        }
            //        Console.WriteLine(response.ReasonPhrase);
            //        Console.WriteLine(response.Content);
            //    }
            //DebugOutput = 1;//(DateTime.Parse(EndDateTime) - DateTime.Parse(StartDateTime)).TotalMilliseconds.ToString();
            try
            {
                var username = Environment.UserName;
                var cpuCount = Environment.ProcessorCount;
                var hostname = Environment.MachineName;
                var platform = Environment.OSVersion.Platform;
                var os = Environment.OSVersion.VersionString;
                var gitBranch = string.Empty;
                using (var repo = new Repository(Environment.CurrentDirectory))
                {
                    gitBranch = repo.Branches.Where(b => !b.IsRemote && b.IsCurrentRepositoryHead).FirstOrDefault().FriendlyName;
                }

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
                        timeTaken = 2,
                        gitBranch
                    };
                    var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
                    var response = httpClient.PostAsync("/metrics", content).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        Log.Error(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            
        }
    }
}
