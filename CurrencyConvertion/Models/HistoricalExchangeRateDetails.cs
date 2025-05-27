using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConvertion.Models
{
    public class HistoricalExchangeRateDetails : BaseEntity
    {
        [ForeignKey(nameof(HistoricalExchangeRate))]
        public Guid HistoricalExchangeRateId { get; set; }
        public HistoricalExchangeRate HistoricalExchangeRate { get; set; }
        public string Date { get; set; }
        public decimal Rate { get; set; }

    }
}
