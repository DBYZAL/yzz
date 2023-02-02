using System.ComponentModel.DataAnnotations;

namespace WesternInn_zilun_youxian_zhihan.ViewModels
{
    public class BookingDistributionViewModel
    {
        [Display(Name = "Room ID")]
        public int RoomID { get; set; }

        [Display(Name = "Number of Bookings")]
        public int NumberOfBookings { get; set; }
    }
}
