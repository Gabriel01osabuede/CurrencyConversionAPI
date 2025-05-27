using CurrencyConvertion.DTOs;
using CurrencyConvertion.ViewModels;

namespace CurrencyConvertion.Interfaces
{
    public interface ICurrencyRateService
    {
        Task<ResultModel<List<CurrencyRateViewModel>>> GetRealTimeCurrencyRate(FilterDto model);
        Task<ResultModel<List<HistoricalCurrencyRateViewModel>>> GetHistoricalCurrencyRateData(FilterDto model);

        Task<ResultModel<string>> CallRealTimeCurrencyRateData();
        Task<ResultModel<string>> CallHistoricalCurrencyRateData();
        Task<ResultModel<string>> CallHistoricalCurrencyRateDataByParams(string baseCurrency, string target, int date);
        Task<ResultModel<ConvertCurrencyVM>> ConvertCurrency(CurrencyConversionDto model);
        Task<ResultModel<HistoricalCurrencyRateViewModel>> GetHistoricalExchangeRates(HistoryRateDto model);
        Task<ResultModel<ConvertCurrencyBySpecificDateResponseVM>> ConvertCurrencyByPastExchangeRate(ConvertByHistoricDate model);
    }

}
