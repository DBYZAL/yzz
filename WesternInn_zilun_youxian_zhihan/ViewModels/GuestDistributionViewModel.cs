using System.ComponentModel.DataAnnotations;

namespace WesternInn_zilun_youxian_zhihan.ViewModels
{
    public class GuestDistributionViewModel
    {
        [Display(Name = "Postal Code")]
        public string PostCode { get; set; }

        [Display(Name = "Number of Guests")]
        public int NumberOfGuests { get; set; }
    }
}
