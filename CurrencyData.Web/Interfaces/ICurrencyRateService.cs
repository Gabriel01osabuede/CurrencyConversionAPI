using CurrencyData.Web.ViewModels;

namespace CurrencyData.Web.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<List<CurrencyRateVM>> RealTimeCurrencyRateData();

        Task<List<HistoricalCurrencyRateVM>> GetHistoricalCurrencyRateData();
        Task<HistoricalCurrencyRateVM> HistoricalRecords(string baseCurrency, string targetCurrency, int year);
    }
}
