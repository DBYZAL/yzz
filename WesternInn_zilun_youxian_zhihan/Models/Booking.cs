using System.ComponentModel.DataAnnotations;

namespace WesternInn_zilun_youxian_zhihan.Models
{
    public class Booking
    {
        public int ID { get; set; }

        public int RoomID { get; set; }

        // foreign key
        [Required]
        [DataType(DataType.EmailAddress)]
        //[EmailAddress], implied above
        public string GuestEmail { get; set; } = string.Empty;

        [Display(Name = "Check In")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckIn { get; set; }

        [Display(Name = "Check Out")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CheckOut { get; set; }

        [Range(0, 10000)]
        public decimal Cost { get; set; }

        [Required]
        public Room? TheRooms { get; set; }

        [Required]
        public Guest? TheGuests { get; set; }

    }
}
