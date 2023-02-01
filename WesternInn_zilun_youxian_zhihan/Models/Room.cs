using System.ComponentModel.DataAnnotations;

namespace WesternInn_zilun_youxian_zhihan.Models
{
    public class Room
    {
        public int ID { get; set; }
        [Required]
        [RegularExpression(@"^[G1-3]{1}$")]

        public string Level { get; set; } = string.Empty;

        [Required]
        [Range(1, 3)]

        public int BedCount { get; set; }

        [Range(50, 300)]
        public decimal Price { get; set; }

        [Required]
        public Booking? TheBookings { get; set; }
    }
}
