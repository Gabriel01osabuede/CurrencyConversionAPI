namespace CurrencyConvertion.DTOs
{
    public class HistoryRateDto
    {
        public required string BaseCurrency { get; set; }
        public required string TargetCurrency { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }



}
