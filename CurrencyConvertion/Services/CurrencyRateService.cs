using CurrencyConvertion.Interfaces;
using CurrencyConvertion.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CurrencyConvertion.Services
{
    public class CurrencyRateService : ICurrencyRateService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CurrencyRateUrl _currencyRateUrl;
        private readonly ILogger<CurrencyRateService> _logger;
        public CurrencyRateService(IHttpClientFactory httpClientFactory, 
        IOptions<CurrencyRateUrl> currencyRateUrl, 
        ILogger<CurrencyRateService> logger)
        {
            _httpClientFactory=httpClientFactory;
            _currencyRateUrl=currencyRateUrl.Value;
            _logger=logger;
        }
        
        public async Task<ResultModel<List<CurrencyRateViewModel>>> GetRealTimeCurrencyRate(FilterViewModel model)
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

        public async Task<ResultModel<List<HistoricalCurrencyRateViewModel>>> GetHistoricalCurrencyRateData(FilterViewModel model)
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

    }
}
