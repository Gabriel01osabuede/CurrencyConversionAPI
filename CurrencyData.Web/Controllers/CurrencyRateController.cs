using CurrencyData.Web.Interfaces;
using CurrencyData.Web.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CurrencyData.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/[action]")]
    public class CurrencyRateController : ControllerBase
    {
        private readonly ICurrencyRateService _currencyRateService;
        public CurrencyRateController(ICurrencyRateService currencyRateService)
        {
            _currencyRateService = currencyRateService;
        }

        //localhost:44352/api/v1/CurrencyRate/RealTimeCurrencyRate
        [HttpGet]
        [ProducesResponseType(typeof(List<CurrencyRateVM>), 200)]
        public async Task<IActionResult> RealTimeCurrencyRate()
        {
            try
            {
                var res = await _currencyRateService.RealTimeCurrencyRateData();

                if (res == null)
                    return BadRequest("Error occurred during API call");


                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }


        //localhost:44352/api/v1/CurrencyRate/HistoricalCurrencyRate
        [HttpGet]
        [ProducesResponseType(typeof(List<HistoricalCurrencyRateVM>), 200)]
        public async Task<IActionResult> HistoricalCurrencyRate()
        {
            try
            {
                var res = await _currencyRateService.GetHistoricalCurrencyRateData();

                if (res == null)
                    return BadRequest("Error occured during API call");

                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }



        //localhost:44352/api/v1/CurrencyRate/GetHistoricalRecordsByParams?base={baseCurrency}&symbols={symbolsQuery}&date={date}
        [HttpGet]
        [ProducesResponseType(typeof(HistoricalCurrencyRateVM), 200)]
        public async Task<IActionResult> GetHistoricalRecordsByParams([FromQuery] string baseCurrency, [FromQuery] string symbols, [FromQuery] int year)
        {
            try
            {
                var res = await _currencyRateService.HistoricalRecords(baseCurrency, symbols, year);

                if (res == null)
                    return BadRequest("Error occurred during API call");


                return Ok(res);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
