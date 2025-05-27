using Azure.Core;
using CurrencyConvertion.Context;
using CurrencyConvertion.DTOs;
using CurrencyConvertion.Interfaces;
using CurrencyConvertion.Models;
using CurrencyConvertion.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CurrencyConvertion.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CurrencyRateUrl _currencyRateUrl;
        private readonly ILogger<CurrencyRateService> _logger;
        private readonly CurrencyDbContext _currencyDbContext;
        public CurrencyRateService(IHttpClientFactory httpClientFactory, 
        IOptions<CurrencyRateUrl> currencyRateUrl, 
        ILogger<CurrencyRateService> logger, CurrencyDbContext currencyDbContext)
        {
            _httpClientFactory=httpClientFactory;
            _currencyRateUrl=currencyRateUrl.Value;
            _logger=logger;
            _currencyDbContext=currencyDbContext;
        }
        
        public async Task<ResultModel<List<CurrencyRateViewModel>>> GetRealTimeCurrencyRate(FilterDto model)
        {
            var result = new ResultModel<List<CurrencyRateViewModel>>();

            var pageSize = model.PageSize == null || model.PageSize < 1 ? 10 : model.PageSize;
            var pageIndex = model.PageIndex == null || model.PageIndex < 1 ? 1 : model.PageIndex;

            try
            {
                //Call Real Time API Method
                var apiCall = await CallRealTimeCurrencyRateData();
                if (apiCall.HasError)
                {
                    result.AddError($"Error occurred during API call: {apiCall.ErrorMessage}");
                    _logger.LogError("API call failed: {Error}", apiCall.ErrorMessage);
                    return result;
                }

                if (string.IsNullOrWhiteSpace(apiCall.Data))
                {
                    result.AddError("API returned empty data.");
                    _logger.LogError("API returned empty data.");
                    return result;
                }

                try
                {

                    var deserializeResponse = JsonConvert.DeserializeObject<List<CurrencyRateViewModel>>(apiCall.Data);
                    if (deserializeResponse == null)
                    {
                        result.AddError("Deserialization failed. No data was returned.");
                        _logger.LogError("Deserialization failed for API response.");
                        return result;
                    }

                    var pagedData = deserializeResponse
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    result.Data = pagedData;
                    result.TotalCount = deserializeResponse.Count;
                    result.Message = "Successful";
                }
                catch (JsonException jex)
                {
                    result.AddError("Error occurred during data deserialization.");
                    _logger.LogError(jex, "Deserialization error: {Message}", jex.Message);
                }
                catch (Exception ex)
                {
                    result.AddError("An unexpected error occurred during deserialization.");
                    _logger.LogError(ex, "Unexpected error during deserialization: {Message}", ex.Message);
                }
            }
            catch (TaskCanceledException tce)
            {
                result.AddError("The request to the external service timed out.");
                _logger.LogError(tce, "Request timed out: {Message}", tce.Message);
            }
            catch (Exception ex)
            {
                result.AddError("An unexpected error occurred while retrieving currency rates.");
                _logger.LogError(ex, "Unexpected error: {Message}", ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<List<HistoricalCurrencyRateViewModel>>> GetHistoricalCurrencyRateData(FilterDto model)
        {
            var result = new ResultModel<List<HistoricalCurrencyRateViewModel>>();

            var pageSize = model.PageSize == null || model.PageSize < 1 ? 10 : model.PageSize;
            var pageIndex = model.PageIndex == null || model.PageIndex < 1 ? 1 : model.PageIndex;

            try
            {


                //Call Real Time API Method
                var apiCall = await CallHistoricalCurrencyRateData();
                if (apiCall.HasError)
                {
                    result.AddError($"Error occured during API call: {apiCall.ErrorMessage}");
                    return result;
                }

                if (string.IsNullOrWhiteSpace(apiCall.Data))
                {
                    result.AddError("API returned empty data.");
                    _logger.LogError("API returned empty data.");
                    return result;
                }

                try
                {
                    var deserializeResponse = JsonConvert.DeserializeObject<List<HistoricalCurrencyRateViewModel>>(apiCall.Data);
                    if (deserializeResponse == null)
                    {
                        result.AddError("Error occured during convertion");
                        return result;
                    }


                    var pagedData = deserializeResponse
                        .Skip((pageIndex - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    result.Data = pagedData;
                    result.TotalCount = deserializeResponse.Count;
                    result.Message = "Successful";
                }
                catch (JsonException jex)
                {
                    result.AddError("Error occurred during data deserialization.");
                    _logger.LogError(jex, "Deserialization error: {Message}", jex.Message);
                }
                catch (Exception ex)
                {
                    result.AddError("An unexpected error occurred during deserialization.");
                    _logger.LogError(ex, "Unexpected error during deserialization: {Message}", ex.Message);
                }
            }
            catch (TaskCanceledException tce)
            {
                result.AddError("The request to the external service timed out.");
                _logger.LogError(tce, "Request timed out: {Message}", tce.Message);
            }
            catch (Exception ex)
            {
                result.AddError("An unexpected error occurred while retrieving currency rates.");
                _logger.LogError(ex, "Unexpected error: {Message}", ex.Message);
            }

            return result;
        }

        public async Task<ResultModel<string>> CallRealTimeCurrencyRateData()
        {
            var result = new ResultModel<string>();

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_currencyRateUrl.BaseUrl);


                // GET request
                var response = await client.GetAsync("RealTimeCurrencyRate");

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"API Call made successfully ==> StatusCode: {response.StatusCode} Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    result.Data = responseContent;
                    _logger.LogInformation($"Content: {responseContent}");
                    result.Message = "Successful";

                }
                else
                {
                    result.AddError($"API call failed with status code: {response.StatusCode}");
                    _logger.LogError($"API call failed with status code: {response.StatusCode}, Message {response.Content}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"API call failed with the following error: {ex.Message}, {ex.InnerException}");
                result.AddError($"Exception occurred: {ex.Message}");
            }

            return result;
        }
        public async Task<ResultModel<string>> CallHistoricalCurrencyRateData()
        {
            var result = new ResultModel<string>();

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_currencyRateUrl.BaseUrl);


                // GET request
                var response = await client.GetAsync("HistoricalCurrencyRate");

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"API Call made successfully ==> StatusCode: {response.StatusCode} Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    result.Data = responseContent;
                    _logger.LogInformation($"Content: {responseContent}");
                    result.Message = "Successful";

                }
                else
                {
                    result.AddError($"API call failed with status code: {response.StatusCode}");
                    _logger.LogError($"API call failed with status code: {response.StatusCode}, Message {response.Content}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"API call failed with the following error: {ex.Message}, {ex.InnerException}");
                result.AddError($"Exception occurred: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<string>> CallHistoricalCurrencyRateDataByParams(string baseCurrency, string target, int date)
        {
            var result = new ResultModel<string>();

            try
            {
                var client = _httpClientFactory.CreateClient();
                client.BaseAddress = new Uri(_currencyRateUrl.BaseUrl);

                // Build the query parameters
                var query = $"GetHistoricalRecordsByParams?baseCurrency={baseCurrency}&symbols={target}&year={date}";

                // GET request
                var response = await client.GetAsync(query);

                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"API Call made successfully ==> StatusCode: {response.StatusCode} Response: {responseContent}");

                if (response.IsSuccessStatusCode)
                {
                    result.Data = responseContent;
                    _logger.LogInformation($"Content: {responseContent}");
                    result.Message = "Successful";

                }
                else
                {
                    result.AddError($"API call failed with status code: {response.StatusCode}");
                    _logger.LogError($"API call failed with status code: {response.StatusCode}, Message {response.Content}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"API call failed with the following error: {ex.Message}, {ex.InnerException}");
                result.AddError($"Exception occurred: {ex.Message}");
            }

            return result;
        }

        public async Task<ResultModel<ConvertCurrencyVM>> ConvertCurrency(CurrencyConversionDto model)
        {
            var result = new ResultModel<ConvertCurrencyVM>();

            if (string.IsNullOrWhiteSpace(model.BaseCurrency) || string.IsNullOrWhiteSpace(model.TargetCurrency))
            {
                result.AddError("BaseCurrency and TargetCurrency are required.");
                return result;
            }

            if (model.Amount <= 0)
            {
                result.AddError("Amount must be positive.");
                return result;
            }

            // Fetch the latest exchange rate record for the base currency
            var latestExchangeRate = await _currencyDbContext.RealTimeExchangeRates
                .Include(r => r.RealTimeExchangeRateDetails)
                .Where(r => r.BaseCurrency.ToUpper() == model.BaseCurrency.ToUpper())
                .OrderByDescending(r => r.Date)
                .FirstOrDefaultAsync();

            if (latestExchangeRate == null)
            {
                result.AddError("Exchange rate data not available for the given base currency.");
                return result;
            }

            var rateDetail = latestExchangeRate.RealTimeExchangeRateDetails
                .FirstOrDefault(d => d.TargetCurrency.ToUpper() == model.TargetCurrency.ToUpper());

            if (rateDetail == null)
            {
                result.AddError("Target currency exchange rate not found for the given base currency.");
                return result;
            }

            var convertedAmount = model.Amount * rateDetail.Rate;

            result.Data = new ConvertCurrencyVM
            {
                OriginalAmount = model.Amount,
                ConvertedAmount = convertedAmount,
                ExchangeRate = rateDetail.Rate
            };

            result.Message = "Successful";
            return result;
        }

        public async Task<ResultModel<HistoricalCurrencyRateViewModel>> GetHistoricalExchangeRates(HistoryRateDto model)
        {
            var result = new ResultModel<HistoricalCurrencyRateViewModel>();

            if (string.IsNullOrWhiteSpace(model.BaseCurrency) || string.IsNullOrWhiteSpace(model.TargetCurrency))
            {
                result.AddError("BaseCurrency and TargetCurrency are required.");
                return result;
            }

            if (model.StartDate > model.EndDate)
            {
                result.AddError("StartDate must be less than or equal to EndDate.");
                return result;
            }


            // Fetch the HistoricalExchangeRate entity
            var historicalExchangeRate = await _currencyDbContext.HistoricalExchangeRates
                .Include(h => h.HistoricalExchangeRateDetails)
                .FirstOrDefaultAsync(h =>
                    h.BaseCurrency.ToUpper() == model.BaseCurrency.ToUpper() &&
                    h.TargetCurrency.ToUpper() == model.TargetCurrency.ToUpper());

            if (historicalExchangeRate == null)
            {
                result.AddError("No historical exchange rate data found for the given currencies.");
                return result;
            }

            // Filter details by date range
            var ratesInRange = historicalExchangeRate.HistoricalExchangeRateDetails
                .Where(d =>
                {
                    if (DateTime.TryParse(d.Date, out var date))
                    {
                        return date.Date >= model.StartDate.Date && date.Date <= model.EndDate.Date;
                    }
                    return false;
                })
                .OrderBy(d => DateTime.Parse(d.Date))
                .ToList();

            if (!ratesInRange.Any())
            {
                result.AddError("No exchange rate data found for the given date range.");
                return result;
            }

            var ratesDict = new Dictionary<DateTime, decimal>();

            foreach (var detail in ratesInRange)
            {
                if (DateTime.TryParse(detail.Date, out var date))
                {
                    ratesDict[date] = detail.Rate;
                }
            }

            result.Data = new HistoricalCurrencyRateViewModel
            {
                Base = model.BaseCurrency.ToUpper(),
                Target = model.TargetCurrency.ToUpper(),
                Rates = ratesDict
            };


            return result;

        }

        public async Task<ResultModel<ConvertCurrencyBySpecificDateResponseVM>> ConvertCurrencyByPastExchangeRate(ConvertByHistoricDate model)
        {
            var result = new ResultModel<ConvertCurrencyBySpecificDateResponseVM>();

            if (string.IsNullOrWhiteSpace(model.BaseCurrency) || string.IsNullOrWhiteSpace(model.TargetCurrency))
            {
                result.AddError("BaseCurrency and TargetCurrency are required.");
                return result;
            }

            if (model.Amount <= 0)
            {
                result.AddError("Amount must be greater than zero.");
            }

            // Find the historical exchange rate entity
            var historicalExchangeRate = await _currencyDbContext.HistoricalExchangeRates
                .Include(h => h.HistoricalExchangeRateDetails)
                .FirstOrDefaultAsync(h =>
                    h.BaseCurrency.ToUpper() == model.BaseCurrency.ToUpper() &&
                    h.TargetCurrency.ToUpper() == model.TargetCurrency.ToUpper());

            if (historicalExchangeRate == null)
            {
                result.AddError("No historical exchange rate data found for the given currencies.");
                return result;
            }

            // Find the rate for the specific date
            var rateDetail = historicalExchangeRate.HistoricalExchangeRateDetails
                .FirstOrDefault(d =>
                {
                    if (DateTime.TryParse(d.Date, out var rateDate))
                    {
                        return rateDate.Date == model.Date.Date;
                    }
                    return false;
                });

            if (rateDetail == null)
            {
                result.AddError("No exchange rate data found for the specified date.");
                return result;
            }

            // Perform conversion
            var convertedAmount = model.Amount * rateDetail.Rate;

            result.Data = new ConvertCurrencyBySpecificDateResponseVM
            {
                BaseCurrency = model.BaseCurrency,
                TargetCurrency = model.TargetCurrency,
                OriginalAmount = model.Amount,
                ConvertedAmount = convertedAmount,
                ExchangeRate = rateDetail.Rate
            };

            result.Message = "Successful";
            return result;
        }
    }
}
