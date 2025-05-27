using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConvertion.Models
{
    public class RealTimeExchangeRateDetails : BaseEntity
    {
        [ForeignKey(nameof(RealTimeExchangeRateDetails))]
        public Guid ExchangeRateId { get; set; }
        public RealTimeExchangeRate ExchangeRate { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
    }
}
