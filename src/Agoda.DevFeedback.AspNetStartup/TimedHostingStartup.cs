using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(Agoda.DevFeedback.AspNetStartup.TimedHostingStartup))]

namespace Agoda.DevFeedback.AspNetStartup
{
    public class TimedHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            TimedStartup.Configure = DateTime.Now;

            builder.ConfigureServices((context, services) =>
            {
                if (context.HostingEnvironment.IsDevelopment())
                {
                    services.AddTransient<IStartupFilter, TimedStartupFilter>();

                    services.AddHostedService<TimedStartupHostedService>();
                }
            });
        }
    }
}
