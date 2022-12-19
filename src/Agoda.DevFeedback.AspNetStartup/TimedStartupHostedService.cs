using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
                var diff = from.Value - until.Value;

                _logger.LogDebug(
                    "Application startup time was {seconds} seconds.",
                    Math.Round(diff.TotalSeconds, 1)
                );

                try
                {
                    TimedStartupPublisher.Publish(type: ".AspNetStartup", timeTaken: diff);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to publish startup time.");
                }
            }
        }
    }
}
