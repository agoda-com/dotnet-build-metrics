using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Agoda.DevFeedback.Common;

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
                    var diff = until.Value - from.Value;

                    _logger.LogInformation(
                        "Application startup time until first response was {duration}ms for {path}",
                        Math.Round(diff.TotalMilliseconds, 0),
                        httpContext.Request.Path
                    );

                    try
                    {
                        TimedStartupPublisher.Publish(type: ".AspNetResponse", timeTaken: diff);
                    }
                    catch (GitContextException ex)
                    {
                        _logger.LogInformation("The startup time until first response will not be published: {reason}", ex.Message);
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
