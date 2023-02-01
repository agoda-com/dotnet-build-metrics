using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Agoda.DevFeedback.Common;

namespace Agoda.DevFeedback.AspNetStartup
{
    public class TimedStartupHostedService : IHostedService
    {
        private readonly ILogger _logger;
        private readonly IHostApplicationLifetime _lifetime;

        public TimedStartupHostedService(
            ILogger<TimedStartupHostedService> logger,
            IHostApplicationLifetime lifetime
        )
        {
            _logger = logger;
            _lifetime = lifetime;
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _lifetime.ApplicationStarted.Register(OnStarted);
            return Task.CompletedTask;
        }

        void OnStarted()
        {
            TimedStartup.Started = DateTime.Now;

            var from = TimedStartup.Configure;
            var until = TimedStartup.Started;

            if (from.HasValue && until.HasValue)
            {
                var diff = until.Value - from.Value;

                _logger.LogInformation(
                    "Application startup time was {duration}ms.",
                    Math.Round(diff.TotalMilliseconds, 0)
                );

                try
                {
                    TimedStartupPublisher.Publish(type: ".AspNetStartup", timeTaken: diff);
                }
                catch (GitContextException ex)
                {
                    _logger.LogInformation("The startup time will not be published: {reason}", ex.Message);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to publish startup time.");
                }
            }
        }
    }
}
