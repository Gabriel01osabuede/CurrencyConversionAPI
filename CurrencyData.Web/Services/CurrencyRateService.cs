using CurrencyData.Web.Interfaces;
using CurrencyData.Web.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CurrencyData.Web.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly ILogger<CurrencyRateService> _logger;
        public CurrencyRateService(ILogger<CurrencyRateService> logger)
        {
            _logger=logger;
        }

        public async Task<List<CurrencyRateVM>> RealTimeCurrencyRateData()
        {
            var result = new List<CurrencyRateVM>();

            result = new List<CurrencyRateVM>
            {
                new CurrencyRateVM
                {
                    Base = "USD",
                    Date = new DateTime(2025, 4, 10),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "GBP", 0.80m },
                        { "EUR", 0.92m },
                        { "JPY", 155.00m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "EUR",
                    Date = new DateTime(2025, 04, 11),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 1.08m },
                        { "GBP", 0.85m },
                        { "JPY", 165.00m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "GBP",
                    Date = new DateTime(2025, 4, 12),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 1.27m },
                        { "EUR", 1.17m },
                        { "JPY", 189.00m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "JPY",
                    Date = new DateTime(2025, 4, 13),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.0064m },
                        { "EUR", 0.0058m },
                        { "GBP", 0.0052m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "AUD",
                    Date = new DateTime(2025, 4, 14),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.66m },
                        { "EUR", 0.61m },
                        { "GBP", 0.54m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "CAD",
                    Date = new DateTime(2025, 4, 15),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.75m },
                        { "EUR", 0.70m },
                        { "GBP", 0.61m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "CHF",
                    Date = new DateTime(2025, 4, 16),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 1.11m },
                        { "EUR", 1.01m },
                        { "GBP", 0.89m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "NZD",
                    Date = new DateTime(2025, 4, 17),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.62m },
                        { "EUR", 0.58m },
                        { "GBP", 0.51m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "SGD",
                    Date = new DateTime(2025, 4, 18),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.74m },
                        { "EUR", 0.69m },
                        { "GBP", 0.60m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "HKD",
                    Date = new DateTime(2025, 4, 19),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.13m },
                        { "EUR", 0.12m },
                        { "GBP", 0.10m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "NOK",
                    Date = new DateTime(2025, 4, 20),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.095m },
                        { "EUR", 0.087m },
                        { "GBP", 0.075m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "SEK",
                    Date = new DateTime(2025, 4, 21),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.094m },
                        { "EUR", 0.086m },
                        { "GBP", 0.074m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "DKK",
                    Date = new DateTime(2025, 4, 22),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.15m },
                        { "EUR", 0.14m },
                        { "GBP", 0.12m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "CNY",
                    Date = new DateTime(2025, 4, 23),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.14m },
                        { "EUR", 0.13m },
                        { "GBP", 0.11m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "INR",
                    Date = new DateTime(2025, 4, 24),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.012m },
                        { "EUR", 0.011m },
                        { "GBP", 0.009m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "ZAR",
                    Date = new DateTime(2025, 4, 25),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.053m },
                        { "EUR", 0.049m },
                        { "GBP", 0.042m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "BRL",
                    Date = new DateTime(2025, 4, 26),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.19m },
                        { "EUR", 0.17m },
                        { "GBP", 0.15m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "MXN",
                    Date = new DateTime(2025, 4, 27),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.059m },
                        { "EUR", 0.054m },
                        { "GBP", 0.046m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "TRY",
                    Date = new DateTime(2025, 4, 28),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.032m },
                        { "EUR", 0.029m },
                        { "GBP", 0.025m }
                    }
                },
                new CurrencyRateVM
                {
                    Base = "PLN",
                    Date = new DateTime(2025, 4, 29),
                    Rates = new Dictionary<string, decimal>
                    {
                        { "USD", 0.25m },
                        { "EUR", 0.23m },
                        { "GBP", 0.20m }
                    }
                }

            };

            return result;
        }

        public async Task<List<HistoricalCurrencyRateVM>> GetHistoricalCurrencyRateData()
        {
            var result = new List<HistoricalCurrencyRateVM>();

            result = new List<HistoricalCurrencyRateVM>
            {
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "GBP",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.79m },
                        { new DateTime(2025, 4, 2), 0.80m },
                        { new DateTime(2025, 4, 3), 0.81m },
                        { new DateTime(2025, 4, 4), 0.80m },
                        { new DateTime(2025, 4, 5), 0.795m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "EUR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.91m },
                        { new DateTime(2025, 4, 2), 0.92m },
                        { new DateTime(2025, 4, 3), 0.93m },
                        { new DateTime(2025, 4, 4), 0.92m },
                        { new DateTime(2025, 4, 5), 0.91m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "EUR",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 165.00m },
                        { new DateTime(2025, 4, 2), 164.50m },
                        { new DateTime(2025, 4, 3), 166.00m },
                        { new DateTime(2025, 4, 4), 165.20m },
                        { new DateTime(2025, 4, 5), 165.80m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "GBP",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 190.00m },
                        { new DateTime(2025, 4, 2), 189.50m },
                        { new DateTime(2025, 4, 3), 191.00m },
                        { new DateTime(2025, 4, 4), 190.20m },
                        { new DateTime(2025, 4, 5), 190.80m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "AUD",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.65m },
                        { new DateTime(2025, 4, 2), 0.66m },
                        { new DateTime(2025, 4, 3), 0.67m },
                        { new DateTime(2025, 4, 4), 0.66m },
                        { new DateTime(2025, 4, 5), 0.65m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "CAD",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.74m },
                        { new DateTime(2025, 4, 2), 0.75m },
                        { new DateTime(2025, 4, 3), 0.76m },
                        { new DateTime(2025, 4, 4), 0.75m },
                        { new DateTime(2025, 4, 5), 0.74m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "CHF",
                    Target = "EUR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 1.00m },
                        { new DateTime(2025, 4, 2), 1.01m },
                        { new DateTime(2025, 4, 3), 1.02m },
                        { new DateTime(2025, 4, 4), 1.01m },
                        { new DateTime(2025, 4, 5), 1.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "NZD",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.61m },
                        { new DateTime(2025, 4, 2), 0.62m },
                        { new DateTime(2025, 4, 3), 0.63m },
                        { new DateTime(2025, 4, 4), 0.62m },
                        { new DateTime(2025, 4, 5), 0.61m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "SGD",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.73m },
                        { new DateTime(2025, 4, 2), 0.74m },
                        { new DateTime(2025, 4, 3), 0.75m },
                        { new DateTime(2025, 4, 4), 0.74m },
                        { new DateTime(2025, 4, 5), 0.73m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "HKD",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.128m },
                        { new DateTime(2025, 4, 2), 0.129m },
                        { new DateTime(2025, 4, 3), 0.13m },
                        { new DateTime(2025, 4, 4), 0.129m },
                        { new DateTime(2025, 4, 5), 0.128m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "NOK",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.093m },
                        { new DateTime(2025, 4, 2), 0.094m },
                        { new DateTime(2025, 4, 3), 0.095m },
                        { new DateTime(2025, 4, 4), 0.094m },
                        { new DateTime(2025, 4, 5), 0.093m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "SEK",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.092m },
                        { new DateTime(2025, 4, 2), 0.093m },
                        { new DateTime(2025, 4, 3), 0.094m },
                        { new DateTime(2025, 4, 4), 0.093m },
                        { new DateTime(2025, 4, 5), 0.092m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "DKK",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.14m },
                        { new DateTime(2025, 4, 2), 0.145m },
                        { new DateTime(2025, 4, 3), 0.15m },
                        { new DateTime(2025, 4, 4), 0.145m },
                        { new DateTime(2025, 4, 5), 0.14m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "CNY",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.14m },
                        { new DateTime(2025, 4, 2), 0.141m },
                        { new DateTime(2025, 4, 3), 0.142m },
                        { new DateTime(2025, 4, 4), 0.141m },
                        { new DateTime(2025, 4, 5), 0.14m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "INR",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.012m },
                        { new DateTime(2025, 4, 2), 0.0121m },
                        { new DateTime(2025, 4, 3), 0.0122m },
                        { new DateTime(2025, 4, 4), 0.0121m },
                        { new DateTime(2025, 4, 5), 0.012m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "ZAR",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.052m },
                        { new DateTime(2025, 4, 2), 0.053m },
                        { new DateTime(2025, 4, 3), 0.054m },
                        { new DateTime(2025, 4, 4), 0.053m },
                        { new DateTime(2025, 4, 5), 0.052m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "BRL",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.185m },
                        { new DateTime(2025, 4, 2), 0.19m },
                        { new DateTime(2025, 4, 3), 0.195m },
                        { new DateTime(2025, 4, 4), 0.19m },
                        { new DateTime(2025, 4, 5), 0.185m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "MXN",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.058m },
                        { new DateTime(2025, 4, 2), 0.059m },
                        { new DateTime(2025, 4, 3), 0.06m },
                        { new DateTime(2025, 4, 4), 0.059m },
                        { new DateTime(2025, 4, 5), 0.058m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "TRY",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.031m },
                        { new DateTime(2025, 4, 2), 0.032m },
                        { new DateTime(2025, 4, 3), 0.033m },
                        { new DateTime(2025, 4, 4), 0.032m },
                        { new DateTime(2025, 4, 5), 0.031m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "PLN",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2025, 4, 1), 0.24m },
                        { new DateTime(2025, 4, 2), 0.245m },
                        { new DateTime(2025, 4, 3), 0.25m },
                        { new DateTime(2025, 4, 4), 0.245m },
                        { new DateTime(2025, 4, 5), 0.24m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "CAD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 1.34m },
                        { new DateTime(2024, 4, 2), 1.35m },
                        { new DateTime(2024, 4, 3), 1.36m },
                        { new DateTime(2024, 4, 4), 1.35m },
                        { new DateTime(2024, 4, 5), 1.34m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 150.50m },
                        { new DateTime(2024, 4, 2), 151.00m },
                        { new DateTime(2024, 4, 3), 151.50m },
                        { new DateTime(2024, 4, 4), 151.00m },
                        { new DateTime(2024, 4, 5), 150.50m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "EUR",
                    Target = "CHF",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 1.05m },
                        { new DateTime(2024, 4, 2), 1.06m },
                        { new DateTime(2024, 4, 3), 1.07m },
                        { new DateTime(2024, 4, 4), 1.06m },
                        { new DateTime(2024, 4, 5), 1.05m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "GBP",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 1.25m },
                        { new DateTime(2024, 4, 2), 1.26m },
                        { new DateTime(2024, 4, 3), 1.27m },
                        { new DateTime(2024, 4, 4), 1.26m },
                        { new DateTime(2024, 4, 5), 1.25m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "AUD",
                    Target = "NZD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 1.08m },
                        { new DateTime(2024, 4, 2), 1.09m },
                        { new DateTime(2024, 4, 3), 1.10m },
                        { new DateTime(2024, 4, 4), 1.09m },
                        { new DateTime(2024, 4, 5), 1.08m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "INR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 83.00m },
                        { new DateTime(2024, 4, 2), 83.10m },
                        { new DateTime(2024, 4, 3), 83.20m },
                        { new DateTime(2024, 4, 4), 83.10m },
                        { new DateTime(2024, 4, 5), 83.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "CNY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 7.25m },
                        { new DateTime(2024, 4, 2), 7.26m },
                        { new DateTime(2024, 4, 3), 7.27m },
                        { new DateTime(2024, 4, 4), 7.26m },
                        { new DateTime(2024, 4, 5), 7.25m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "MXN",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 17.50m },
                        { new DateTime(2024, 4, 2), 17.60m },
                        { new DateTime(2024, 4, 3), 17.70m },
                        { new DateTime(2024, 4, 4), 17.60m },
                        { new DateTime(2024, 4, 5), 17.50m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "BRL",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 4.90m },
                        { new DateTime(2024, 4, 2), 4.91m },
                        { new DateTime(2024, 4, 3), 4.92m },
                        { new DateTime(2024, 4, 4), 4.91m },
                        { new DateTime(2024, 4, 5), 4.90m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "USD",
                    Target = "ZAR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 18.00m },
                        { new DateTime(2024, 4, 2), 18.10m },
                        { new DateTime(2024, 4, 3), 18.20m },
                        { new DateTime(2024, 4, 4), 18.10m },
                        { new DateTime(2024, 4, 5), 18.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "EUR",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 160.00m },
                        { new DateTime(2024, 4, 2), 160.50m },
                        { new DateTime(2024, 4, 3), 161.00m },
                        { new DateTime(2024, 4, 4), 160.50m },
                        { new DateTime(2024, 4, 5), 160.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "CHF",
                    Target = "USD",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 1.10m },
                        { new DateTime(2024, 4, 2), 1.11m },
                        { new DateTime(2024, 4, 3), 1.12m },
                        { new DateTime(2024, 4, 4), 1.11m },
                        { new DateTime(2024, 4, 5), 1.10m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "NZD",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 90.00m },
                        { new DateTime(2024, 4, 2), 90.50m },
                        { new DateTime(2024, 4, 3), 91.00m },
                        { new DateTime(2024, 4, 4), 90.50m },
                        { new DateTime(2024, 4, 5), 90.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "GBP",
                    Target = "EUR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 1.15m },
                        { new DateTime(2024, 4, 2), 1.16m },
                        { new DateTime(2024, 4, 3), 1.17m },
                        { new DateTime(2024, 4, 4), 1.16m },
                        { new DateTime(2024, 4, 5), 1.15m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "SGD",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 110.00m },
                        { new DateTime(2024, 4, 2), 110.50m },
                        { new DateTime(2024, 4, 3), 111.00m },
                        { new DateTime(2024, 4, 4), 110.50m },
                        { new DateTime(2024, 4, 5), 110.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "CAD",
                    Target = "JPY",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 115.00m },
                        { new DateTime(2024, 4, 2), 115.50m },
                        { new DateTime(2024, 4, 3), 116.00m },
                        { new DateTime(2024, 4, 4), 115.50m },
                        { new DateTime(2024, 4, 5), 115.00m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "NOK",
                    Target = "EUR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 0.085m },
                        { new DateTime(2024, 4, 2), 0.086m },
                        { new DateTime(2024, 4, 3), 0.087m },
                        { new DateTime(2024, 4, 4), 0.086m },
                        { new DateTime(2024, 4, 5), 0.085m }
                    }
                },
                new HistoricalCurrencyRateVM
                {
                    Base = "DKK",
                    Target = "EUR",
                    Rates = new Dictionary<DateTime, decimal>
                    {
                        { new DateTime(2024, 4, 1), 0.13m },
                        { new DateTime(2024, 4, 2), 0.131m },
                        { new DateTime(2024, 4, 3), 0.132m },
                        { new DateTime(2024, 4, 4), 0.131m },
                        { new DateTime(2024, 4, 5), 0.13m }
                    }
                }
            };
            return result;
        }

        public async Task<HistoricalCurrencyRateVM> HistoricalRecords(string baseCurrency, string targetCurrency, int year)
        {
            var result = new HistoricalCurrencyRateVM();

            var dataSet = await GetHistoricalCurrencyRateData();

            // Find the matching record
            var matchedRecord = dataSet.FirstOrDefault(r =>
                r.Base.Equals(baseCurrency, StringComparison.OrdinalIgnoreCase) &&
                r.Target.Equals(targetCurrency, StringComparison.OrdinalIgnoreCase));

            if (matchedRecord != null)
            {
                // Filter rates for the specific year
                var filteredRates = matchedRecord.Rates
                    .Where(r => r.Key.Year == year)
                    .ToDictionary(r => r.Key, r => r.Value);

                if (filteredRates.Any())
                {
                    var record = new HistoricalCurrencyRateVM
                    {
                        Base = matchedRecord.Base,
                        Target = matchedRecord.Target,
                        Rates = filteredRates
                    };

                    result = record;
                }
                else
                {
                    // If record not found, log feedback.
                    _logger.LogInformation("Records Not Found");
                }
            }


            return result;
        }
    }
}
