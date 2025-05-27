namespace CurrencyData.Web.ViewModels
{
    public class CurrencyRateVM
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }

    }
}
