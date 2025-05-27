using System.ComponentModel.DataAnnotations;

namespace CurrencyConvertion.Models
{
    public class RealTimeExchangeRate : BaseEntity
    {
        public string BaseCurrency { get; set; }
        public DateTime Date { get; set; }
        public ICollection<RealTimeExchangeRateDetails> RealTimeExchangeRateDetails { get; set; }
    }
}
