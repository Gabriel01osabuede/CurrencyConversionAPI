namespace CurrencyConvertion.DTOs
{
    public class ConvertByHistoricDate
    {
        public required string BaseCurrency { get; set; }
        public required string TargetCurrency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

    }
}
