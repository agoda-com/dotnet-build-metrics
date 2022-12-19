using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Agoda.DevFeedback.AspNetStartup
{
    public class TimedStartupMiddleware
    {
        readonly ILogger<TimedStartupMiddleware> _logger;
        readonly RequestDelegate _next;

        public TimedStartupMiddleware(
            ILogger<TimedStartupMiddleware> logger,
            RequestDelegate next
        )
        {
            _logger = logger;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            await _next(httpContext);

            if (!TimedStartup.Until.HasValue)
            {
                TimedStartup.Until = DateTime.Now;

                var from = TimedStartup.From;
                var until = TimedStartup.Until;

                if (from.HasValue && until.HasValue)
                {
                    var diff = from.Value - until.Value;

                    _logger.LogDebug(
                        "Startup time was {seconds} seconds for {path}",
                        Math.Round(diff.TotalSeconds, 1),
                        httpContext.Request.Path
                    );

                    try
                    {
                        TimedStartupPublisher.Publish(diff);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to publish startup time.");
                    }
                }
            }
        }
    }
}
