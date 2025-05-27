
using CurrencyConvertion.Context;
using CurrencyConvertion.Interfaces;
using CurrencyConvertion.Models;
using CurrencyConvertion.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace CurrencyConvertion.Services.BackgroundJobs
{
    public class HistoricalRateFetchService : BackgroundService
    {
        private readonly ILogger<ExchangeRateFetchService> _logger;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly List<(string baseCurrency, string targetCurrency)> _currencyPairs = new()
        {
            ("USD", "GBP"),
            ("USD", "CAD"),
            ("USD", "CNY"),
            ("GBP", "EUR"),
            ("EUR", "CHF")
        };
        public HistoricalRateFetchService(ILogger<ExchangeRateFetchService> logger,
        IServiceScopeFactory scopeFactory
        )
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting Background HistoricalRateFetchService.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await FetchAndStoreHistoricalRatesAsync();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error fetching historical rates data");
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Every 24 hours
            }

            _logger.LogInformation("HistoricalRateFetchService was stopped.");
        }

        private async Task FetchAndStoreHistoricalRatesAsync()
        {
            _logger.LogInformation("Fetching historical exchange rates data...");

            var todaysDate = DateTime.Now.Date;

            var response = new ResultModel<string>();

            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<CurrencyDbContext>();

                var currencyRateService = scope.ServiceProvider.GetRequiredService<ICurrencyRateService>();


                foreach (var (baseCurrency, targetCurrency) in _currencyPairs)
                {
                    try
                    {
                        var lastYear = todaysDate.AddYears(-1).Year;
                        response = await currencyRateService.CallHistoricalCurrencyRateDataByParams(baseCurrency, targetCurrency, lastYear);
                            
                        
                        // Call the API once for the entire date range

                        if (response.HasError || string.IsNullOrWhiteSpace(response.Data))
                        {
                            _logger.LogWarning("Failed to fetch rates for {0}/{1}", baseCurrency, targetCurrency);
                            continue;
                        }

                        var historicalRates = JsonConvert.DeserializeObject<HistoricalCurrencyRateViewModel>(response.Data);

                        if (historicalRates == null || historicalRates.Rates == null || !historicalRates.Rates.Any())
                        {
                            _logger.LogWarning("No data returned for {0}/{1}, for the selected date {date}", baseCurrency, targetCurrency, lastYear);
                            continue;
                        }

                        // Check if the main record exists
                        var historicalExchangeRate = await dbContext.HistoricalExchangeRates.Include(x => x.HistoricalExchangeRateDetails)
                            .FirstOrDefaultAsync(r => r.BaseCurrency == baseCurrency && r.TargetCurrency == targetCurrency);

                        if (historicalExchangeRate == null)
                        {
                            historicalExchangeRate = new HistoricalExchangeRate
                            {
                                BaseCurrency = baseCurrency,
                                TargetCurrency = targetCurrency,
                                HistoricalExchangeRateDetails = new List<HistoricalExchangeRateDetails>()
                            };

                            await dbContext.HistoricalExchangeRates.AddAsync(historicalExchangeRate);
                        }

                        // Insert missing rates only
                        foreach (var kvp in historicalRates.Rates)
                        {
                            var date = kvp.Key;
                            var rate = kvp.Value;

                            var existingDetail = historicalExchangeRate.HistoricalExchangeRateDetails
                                .FirstOrDefault(d => d.Date == date.ToString("yyyy-MM-dd"));

                            if (existingDetail != null)
                            {
                                _logger.LogInformation("Rate for {0}/{1} on {2} already exists.", baseCurrency, targetCurrency, date.ToShortDateString());
                                continue;
                            }

                            historicalExchangeRate.HistoricalExchangeRateDetails.Add(new HistoricalExchangeRateDetails
                            {
                                Date = date.ToString("yyyy-MM-dd"),
                                Rate = rate
                            });
                        }

                        await dbContext.SaveChangesAsync();
                        _logger.LogInformation("Stored historical rates for {0}/{1}.", baseCurrency, targetCurrency);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing rates for {0}/{1}", baseCurrency, targetCurrency);
                    }

                }
            }
        }

    }
}
