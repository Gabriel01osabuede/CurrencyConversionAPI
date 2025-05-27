using System.ComponentModel.DataAnnotations;

namespace CurrencyConvertion.ViewModels
{
    public class CurrencyRateViewModel
    {   
        public string Base { get; set; }
        [DisplayFormat(DataFormatString = "yyyy-mm-dd")]
        public DateTime date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; }
    }
}
