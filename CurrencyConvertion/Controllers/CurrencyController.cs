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
        public async Task<IActionResult> RealTimeCurrencyRate([FromQuery] FilterViewModel model)
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
        public async Task<IActionResult> HistoricalCurrencyRate([FromQuery] FilterViewModel model)
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
    }
}
