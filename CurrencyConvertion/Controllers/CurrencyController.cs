using CurrencyConvertion.DTOs;
using CurrencyConvertion.Enums;
using CurrencyConvertion.Interfaces;
using CurrencyConvertion.Services;
using CurrencyConvertion.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConvertion.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiVersion("1.0")]
    [ServiceFilter(typeof(ApiKeyAuthAndRetryAttribute))]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRateService _currencyRateService;
        public CurrencyController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }


        
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<CurrencyRateViewModel>>), 200)]
        public async Task<IActionResult> RealTimeCurrencyRate([FromQuery] FilterDto model)
        {
            try
            {                
                var res = await _currencyRateService.GetRealTimeCurrencyRate(model);

                if (res.HasError)
                    return BadRequest(new ApiResponse<string>(errors: res.ErrorMessages.ToArray(), codes: ApiResponseCodes.FAIL));

                return Ok(new ApiResponse<List<CurrencyRateViewModel>>(data: res.Data, res.Message, ApiResponseCodes.OK, totalCount: res.TotalCount));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(errors: ex.Message, codes: ApiResponseCodes.FAIL));
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<HistoricalCurrencyRateViewModel>>), 200)]
        public async Task<IActionResult> HistoricalCurrencyRate([FromQuery] FilterDto model)
        {
            try
            {
                var res = await _currencyRateService.GetHistoricalCurrencyRateData(model);

                if (res.HasError)
                    return BadRequest(new ApiResponse<string>(errors: res.ErrorMessages.ToArray(), codes: ApiResponseCodes.FAIL));

                return Ok(new ApiResponse<List<HistoricalCurrencyRateViewModel>>(data: res.Data, res.Message, ApiResponseCodes.OK, totalCount: res.TotalCount));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(errors: ex.Message, codes: ApiResponseCodes.FAIL));
            }
        }



        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ConvertCurrencyVM>), 200)]
        public async Task<IActionResult> ConvertCurrency([FromQuery] CurrencyConversionDto model)
        {
            try
            {
                var res = await _currencyRateService.ConvertCurrency(model);

                if (res.HasError)
                    return BadRequest(new ApiResponse<string>(errors: res.ErrorMessages.ToArray(), codes: ApiResponseCodes.FAIL));

                return Ok(new ApiResponse<ConvertCurrencyVM>(data: res.Data, res.Message, ApiResponseCodes.OK, totalCount: res.TotalCount));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(errors: ex.Message, codes: ApiResponseCodes.FAIL));
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<HistoricalCurrencyRateViewModel>), 200)]
        public async Task<IActionResult> CurrencyExchangeRateHistory([FromQuery] HistoryRateDto model)
        {
            try
            {
                var res = await _currencyRateService.GetHistoricalExchangeRates(model);

                if (res.HasError)
                    return BadRequest(new ApiResponse<string>(errors: res.ErrorMessages.ToArray(), codes: ApiResponseCodes.FAIL));

                return Ok(new ApiResponse<HistoricalCurrencyRateViewModel>(data: res.Data, res.Message, ApiResponseCodes.OK, totalCount: res.TotalCount));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(errors: ex.Message, codes: ApiResponseCodes.FAIL));
            }
        }


        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<ConvertCurrencyBySpecificDateResponseVM>), 200)]
        public async Task<IActionResult> ConvertCurrencyByDate([FromQuery] ConvertByHistoricDate model)
        {
            try
            {
                var res = await _currencyRateService.ConvertCurrencyByPastExchangeRate(model);

                if (res.HasError)
                    return BadRequest(new ApiResponse<string>(errors: res.ErrorMessages.ToArray(), codes: ApiResponseCodes.FAIL));

                return Ok(new ApiResponse<ConvertCurrencyBySpecificDateResponseVM>(data: res.Data, res.Message, ApiResponseCodes.OK, totalCount: res.TotalCount));
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse<string>(errors: ex.Message, codes: ApiResponseCodes.FAIL));
            }
        }
    }
}
