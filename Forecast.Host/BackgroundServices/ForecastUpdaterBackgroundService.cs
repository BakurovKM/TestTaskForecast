using System;
using System.Threading;
using System.Threading.Tasks;
using Forecast.Application.Services.Abstractions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Forecast.Host.BackgroundServices
{
    public class ForecastUpdaterBackgroundService: BackgroundService
    {
        private readonly IGismeteoParser _gismeteoParser;
        private readonly ILogger<ForecastUpdaterBackgroundService> _logger;

        public ForecastUpdaterBackgroundService(IGismeteoParser gismeteoParser,
            ILogger<ForecastUpdaterBackgroundService> logger)
        {
            _gismeteoParser = gismeteoParser;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"ForecastUpdaterHostedService started {DateTime.Now}");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation($"Parse {DateTime.Now}");

                try
                {
                    await _gismeteoParser.ParseAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError($"Parse error: {e.Message}");
                }
                
                await Task.Delay(TimeSpan.FromMinutes(60));
            }
        }
    }
}