namespace CurrencyConvertion.DTOs
{
    public class CurrencyConversionDto
    {
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Amount { get; set; }
    }
}
