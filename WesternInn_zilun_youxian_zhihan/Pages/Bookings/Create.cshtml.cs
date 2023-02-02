using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

using WesternInn_zilun_youxian_zhihan.Data;
using WesternInn_zilun_youxian_zhihan.Models;

namespace WesternInn_zilun_youxian_zhihan.Pages.Bookings
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext _context;
        [BindProperty(SupportsGet = true)]
        public Booking Booking { get; set; }

        [BindProperty]
        public string Message { get; set; }

        public CreateModel(WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext context)
        {
            _context = context;
            Booking = new Booking { TheGuests = new Guest(), TheRooms = new Room() };

        }

        [HttpGet]
        public async Task<IActionResult> OnGetAsync(int? searchedRoomID, DateTime? checkInDate, DateTime? checkOutDate)
        {
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            ViewData["GuestId"] = new SelectList(_context.Guest, "Email", "FullName");
            Booking.GuestEmail = User.Identity.Name;

            if (searchedRoomID.HasValue && checkInDate.HasValue && checkOutDate.HasValue)
            {

                Booking.RoomID = searchedRoomID.Value;
                Booking.CheckIn = checkInDate.Value;
                Booking.CheckOut = checkOutDate.Value;

            }
            else
            {

                Booking.CheckIn = DateTime.Now.AddDays(1);
                Booking.CheckOut = DateTime.Now.AddDays(2);
            }

            return Page();
        }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {

            Room room = await _context.Room.Where(r => r.ID == Booking.RoomID).FirstOrDefaultAsync();
            Guest guest = await _context.Guest.Where(r => r.Email == Booking.GuestEmail).FirstOrDefaultAsync();

            string sql = "select * from Room where ID = @ID and ID not in (select RoomId from Booking where CheckIn >= @CheckIn and CheckOut <= @CheckOut)";
            var parameter1 = new SqliteParameter("@ID", Booking.RoomID);
            var parameter2 = new SqliteParameter("@CheckIn", Booking.CheckIn);
            var parameter3 = new SqliteParameter("@CheckOut", Booking.CheckOut);

            var Rooms = await _context.Room.FromSqlRaw(sql, parameter1, parameter2, parameter3).ToListAsync();
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            ViewData["GuestId"] = new SelectList(_context.Guest, "Email", "FullName");

            if (Rooms.Count() == 0)
            {
                Message = "The selected Room is not available. Please choose a different room, or choose different dates";
                return Page();
            }

            if (Booking.CheckOut <= Booking.CheckIn)
            {
                Message = "Check Out Date must be after Check In Date";
                return Page();
            }

            if (User.IsInRole("Guests"))
            {
                TimeSpan duration = Booking.CheckOut - Booking.CheckIn;
                Booking.Cost = duration.Days * room.Price;
            }

            _context.Booking.Add(Booking);
            await _context.SaveChangesAsync();
            Message = $"The Room # {Booking.RoomID} ({room.BedCount} beds), with Check In Date {Booking.CheckIn:yyyy-MM-dd} and Check Out Date {Booking.CheckOut:yyyy-MM-dd} has been successfully booked to {guest.FullName} for the cost of $ {Booking.Cost: #,##0.00}";
            //return RedirectToPage("./Index");
            return Page();
        }
    }
}
