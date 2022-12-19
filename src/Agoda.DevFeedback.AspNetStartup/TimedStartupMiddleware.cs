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

            if (!TimedStartup.Response.HasValue)
            {
                TimedStartup.Response = DateTime.Now;

                var from = TimedStartup.Configure;
                var until = TimedStartup.Response;

                if (from.HasValue && until.HasValue)
                {
                    var diff = from.Value - until.Value;

                    _logger.LogDebug(
                        "Application startup time until first response was {seconds} seconds for {path}",
                        Math.Round(diff.TotalSeconds, 1),
                        httpContext.Request.Path
                    );

                    try
                    {
                        TimedStartupPublisher.Publish(type: ".AspNetResponse", timeTaken: diff);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Failed to publish first response time.");
                    }
                }
            }
        }
    }
}
