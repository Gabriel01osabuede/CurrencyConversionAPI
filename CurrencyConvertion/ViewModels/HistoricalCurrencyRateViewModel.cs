namespace CurrencyConvertion.ViewModels
{
    public class HistoricalCurrencyRateViewModel
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public Dictionary<DateTime, decimal> Rates { get; set; }

    }
}
