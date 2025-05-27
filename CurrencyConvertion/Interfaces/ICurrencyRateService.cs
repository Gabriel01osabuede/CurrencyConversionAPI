using CurrencyConvertion.ViewModels;

namespace CurrencyConvertion.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<ResultModel<List<CurrencyRateViewModel>>> GetRealTimeCurrencyRate(FilterViewModel model);
        Task<ResultModel<List<HistoricalCurrencyRateViewModel>>> GetHistoricalCurrencyRateData(FilterViewModel model);

        Task<ResultModel<string>> CallRealTimeCurrencyRateData();
        Task<ResultModel<string>> CallHistoricalCurrencyRateData();
        Task<ResultModel<string>> CallHistoricalCurrencyRateDataByParams(string baseCurrency, string target, int date);
    }
}
