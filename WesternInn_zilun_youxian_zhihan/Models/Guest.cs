using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WesternInn_zilun_youxian_zhihan.Models
{
    public class Guest
    {
        public int Id { get; set; }


        [Display(Name = "Email Address")]
        [Key,Required(ErrorMessage = "Each customer must have a Email address")]
        [EmailAddress]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Surname")]
        [RegularExpression(@"[a-zA-Z'-]{2,20}", ErrorMessage = "can only consists of English letters, hyphen and apostrophe, and has a length between 2 characters and 20 characters.")]
        public string Surname { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Given Name")]
        [RegularExpression(@"[a-zA-Z'-]{2,20}", ErrorMessage = "can only consists of English letters, hyphen and apostrophe, and has a length between 2 characters and 20 characters.")]
        public string GivenName { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.PostalCode)]
        [RegularExpression(@"[0-9]{4}", ErrorMessage = "required and consists of exactly 4 digits")]
        public string PostCode { get; set; } = String.Empty;

        // Navigation properties
        public ICollection<Booking>? TheBookings { get; set; }
    }
}
