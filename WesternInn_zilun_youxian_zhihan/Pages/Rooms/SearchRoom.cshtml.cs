using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using WesternInn_zilun_youxian_zhihan.Data;
using WesternInn_zilun_youxian_zhihan.Models;
using WesternInn_zilun_youxian_zhihan.ViewModels;

namespace WesternInn_zilun_youxian_zhihan.Pages.Rooms
{
    [Authorize(Roles = "Guests")]
    public class SearchModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ICollection<Room> Rooms { get; set; }

        public SearchModel(ApplicationDbContext context)
        {
            _context = context;
            Room = new SearchRoom();

        }

        [BindProperty]
        public SearchRoom Room { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Room.CheckInDate > Room.CheckOutDate)
            {
                //throw new Exception("Checkout date must be greater than Check In date.");
            }

            string sql = "select * from Room where BedCount = @BedCount and Id not in (select RoomId from Booking where CheckIn >= @CheckIn and CheckOut <= @CheckOut)";
            var parameter1 = new SqliteParameter("@BedCount", Room.NumberOfBeds);
            var parameter2 = new SqliteParameter("@CheckIn", Room.CheckInDate);
            var parameter3 = new SqliteParameter("@CheckOut", Room.CheckOutDate);

            Rooms = await _context.Room.FromSqlRaw(sql, parameter1, parameter2, parameter3).ToListAsync();


            return Page();

        }
    }
}
