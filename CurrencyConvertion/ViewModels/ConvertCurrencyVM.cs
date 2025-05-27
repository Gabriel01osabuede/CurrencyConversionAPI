namespace CurrencyConvertion.ViewModels
{
    public class ConvertCurrencyVM
    {
        public decimal OriginalAmount { get; set; }
        public decimal ConvertedAmount { get; set; }
        public decimal ExchangeRate { get; set; }
    }


    public class ConvertCurrencyBySpecificDateResponseVM : ConvertCurrencyVM
    {
        public required string BaseCurrency { get; set; }
        public required string TargetCurrency { get; set; }
    }
}
