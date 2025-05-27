namespace CurrencyConvertion.Models
{
    public class HistoricalExchangeRate : BaseEntity
    {
        public required string BaseCurrency { get; set; }
        public required string TargetCurrency { get; set; }
        public ICollection<HistoricalExchangeRateDetails> HistoricalExchangeRateDetails { get; set; }
    }
}
