using System.ComponentModel.DataAnnotations;
namespace WesternInn_zilun_youxian_zhihan.ViewModels
{
    public class SearchRoom
    {
        [Required]
        [Display(Name = "Number of beds")]
        public int NumberOfBeds { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check In Date")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime CheckInDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Check Out Date")]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd", ApplyFormatInEditMode = true)]
        public DateTime CheckOutDate { get; set; }

    }
}
