using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WesternInn_zilun_youxian_zhihan.Data;
using WesternInn_zilun_youxian_zhihan.Models;

namespace WesternInn_zilun_youxian_zhihan.Pages.Bookings
{
    [Authorize(Roles = "Administrator")]

    public class EditModel : PageModel
    {
        private readonly WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext _context;

        public EditModel(WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Booking Booking { get; set; } = default!;
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Booking == null)
            {
                return NotFound();
            }

            var booking = await _context.Booking
                .Include(b => b.TheRooms)
                .Include(b => b.TheGuests)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }
            Booking = booking;
           ViewData["GuestEmail"] = new SelectList(_context.Guest, "Email", "FullName");
           ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            ViewData["GuestEmail"] = new SelectList(_context.Guest, "Email", "FullName");
            ViewData["RoomID"] = new SelectList(_context.Room, "ID", "ID");

            Guest guest = await _context.Guest.Where(g => g.Email == Booking.GuestEmail).FirstOrDefaultAsync();
            Room room = await _context.Room.Where(r => r.ID == Booking.RoomID).FirstOrDefaultAsync();

            string sql = "select * from Room where ID = @ID and ID not in (select RoomID from Booking where ID != @BookingID and CheckIn >= @CheckIn and CheckOut <= @CheckOut)";
            var parameter1 = new SqliteParameter("@ID", Booking.RoomID);
            var parameter2 = new SqliteParameter("@CheckIn", Booking.CheckIn);
            var parameter3 = new SqliteParameter("@CheckOut", Booking.CheckOut);
            var parameter4 = new SqliteParameter("@BookingID", Booking.ID);

            var Rooms = await _context.Room.FromSqlRaw(sql, parameter1, parameter2, parameter3, parameter4).ToListAsync();

            if (Booking.CheckIn >= Booking.CheckOut)
            {
                Message = "Check In Date must be before Check out date.";
                return Page();
            }

            if (Rooms.Count() == 0)
            {
                Message = "Unable to update this booking because it confilicts with another existing booking.";
                return Page();
            }


            _context.Attach(Booking).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                Message = $"This booking has been updated. Room ID {room.ID} ({room.BedCount} beds) Check in on {Booking.CheckIn: yyyy-MM-dd} and Check out on {Booking.CheckOut: yyyy-MM-dd} for guest {guest.FullName} at cost {Booking.Cost}";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingExists(Booking.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Index");
            //return Page();
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.ID == id);
        }
    }
}
