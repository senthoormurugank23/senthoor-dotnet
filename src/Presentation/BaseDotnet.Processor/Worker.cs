using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BaseDotnet.Application.Interfaces;

namespace BaseDotnet.Processor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background Processor started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Background Processor checking for tasks at: {time}", DateTimeOffset.Now);

                try
                {
                    // Create a service scope to resolve scoped services like IProductService or DbContext
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var productService = scope.ServiceProvider.GetRequiredService<IProductService>();
                        var result = await productService.GetAllAsync(stoppingToken);

                        if (result.IsSuccess)
                        {
                            _logger.LogInformation("Successfully processed scheduled task. Total products retrieved: {Count}", result.Value?.Count ?? 0);
                        }
                        else
                        {
                            _logger.LogWarning("Failed to retrieve products: {Error}", result.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while executing the background task.");
                }

                // Run every 10 seconds for demo purposes
                await Task.Delay(10000, stoppingToken);
            }

            _logger.LogInformation("Background Processor stopping.");
        }
    }
}
