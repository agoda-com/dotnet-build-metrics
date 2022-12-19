using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Agoda.DevFeedback.AspNetStartup
{
    public class TimedStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder =>
            {
                builder.UseMiddleware<TimedStartupMiddleware>();
                next(builder);
            };
        }
    }
}
