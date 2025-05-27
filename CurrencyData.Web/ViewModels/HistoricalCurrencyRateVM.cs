namespace CurrencyData.Web.ViewModels
{
    public class HistoricalCurrencyRateVM
    {
        public string Base { get; set; }
        public string Target { get; set; }
        public Dictionary<DateTime, decimal> Rates { get; set; }
    }
}
