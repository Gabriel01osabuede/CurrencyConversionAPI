using CurrencyConvertion.Context;
using CurrencyConvertion.Interfaces;
using CurrencyConvertion.Models;
using CurrencyConvertion.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CurrencyConvertion.Services.BackgroundJobs
{
    public class ExchangeRateFetchService : BackgroundService
    {
        private readonly ILogger<ExchangeRateFetchService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
       

        public ExchangeRateFetchService(ILogger<ExchangeRateFetchService> logger,
        IServiceScopeFactory scopeFactory
        )
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Staring Background ExchangeRateFetchService To Pull RealTime Records.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await FetchAndStoreRatesDataAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching exchange rates data");
                }

                await Task.Delay(TimeSpan.FromHours(3), stoppingToken); // Fetch every 3 hour
            }

            _logger.LogInformation("ExchangeRateFetchService was stopped.");
        }

        private async Task FetchAndStoreRatesDataAsync()
        {
            _logger.LogInformation("Fetching realtime exchange rates record...");

            var response = new ResultModel<string>();

            using (var scope = _scopeFactory.CreateScope())
            {
                var currencyRateService = scope.ServiceProvider.GetRequiredService<ICurrencyRateService>();

                // Method Call
                response = await currencyRateService.CallRealTimeCurrencyRateData();
            }

            if (response.HasError)
            {
                _logger.LogError("API call failed: {Error}", response.ErrorMessage);
                return;
            }

            if (string.IsNullOrWhiteSpace(response.Data))
            {
                _logger.LogError("API returned empty data.");
                return;
            }

            var deserializeResponse = JsonConvert.DeserializeObject<List<CurrencyRateViewModel>>(response.Data);
            if (deserializeResponse == null)
            {
                _logger.LogError("Deserialization failed for API response.");
                return;
            }

            // Save to database
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();

                foreach (var ExchangeRate in deserializeResponse)
                {
                    try
                    {
                        // Check if rates for the date already exist to avoid duplicates
                        var existing = await dbContext.RealTimeExchangeRates.FirstOrDefaultAsync(r => r.BaseCurrency == ExchangeRate.Base && r.Date == ExchangeRate.date);

                        if (existing != null)
                        {
                            _logger.LogInformation("Exchange rates for this date already exist. Updating...");

                            //Update Implementation....
                        }
                        else
                        {
                            var exchangeRate = new RealTimeExchangeRate
                            {
                                BaseCurrency = ExchangeRate.Base,
                                Date = ExchangeRate.date,
                                RealTimeExchangeRateDetails = ExchangeRate.Rates.Select(r => new RealTimeExchangeRateDetails
                                {
                                    TargetCurrency = r.Key,
                                    Rate = r.Value
                                }).ToList()
                            };

                            await dbContext.RealTimeExchangeRates.AddAsync(exchangeRate);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while processing exchange rates.");
                        throw;
                    }
                }

                await dbContext.SaveChangesAsync();
                _logger.LogInformation("Exchange rates saved to the database.");
            }
        }

     
    }
}
