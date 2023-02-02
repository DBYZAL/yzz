using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_zilun_youxian_zhihan.Data;
using WesternInn_zilun_youxian_zhihan.Models;

namespace WesternInn_zilun_youxian_zhihan.Pages.Bookings
{
    public class IndexModel : PageModel
    {
        private readonly WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Booking> Booking { get;set; } = default!;
        public bool IsCheckInOrderActive { get; set; }
        public bool IsCheckOutOrderActive { get; set; }
        public bool IsCheckInDecending { get; set; }
        public bool IsCheckOutDecending { get; set; }
        public bool IsCostOrderActive { get; set; }
        public bool IsCostDecending { get; set; }

        public async Task OnGetAsync(string? sortOrder)
        {
            IsCheckInDecending = false;
            IsCheckOutOrderActive = false;
            IsCheckOutDecending = false;
            IsCheckInOrderActive = false;
            IsCostOrderActive = false;
            IsCostDecending = false;

            if (string.IsNullOrEmpty(sortOrder)) sortOrder = "checkin_asc";


            if (_context.Booking != null)
            {
                var bookings = (IQueryable<Booking>)_context.Booking;
                switch (sortOrder)
                {
                    case "checkin_asc":
                        bookings = bookings.OrderBy(b => b.CheckIn);
                        IsCheckInOrderActive = true;
                        break;
                    case "checkin_desc":
                        bookings = bookings.OrderByDescending(b => b.CheckIn);
                        IsCheckInOrderActive = true;
                        IsCheckInDecending = true;
                        break;
                    case "checkout_asc":
                        bookings = bookings.OrderBy(b => b.CheckOut);
                        IsCheckOutOrderActive = true;
                        break;
                    case "checkout_desc":
                        bookings = bookings.OrderByDescending(b => b.CheckOut);
                        IsCheckOutOrderActive = true;
                        IsCheckOutDecending = true;
                        break;
                    case "cost_asc":
                        bookings = bookings.OrderBy(b => (int)b.Cost);
                        IsCostOrderActive = true;
                        break;
                    case "cost_desc":
                        bookings = bookings.OrderByDescending(b => (int)b.Cost);
                        IsCostOrderActive = true;
                        IsCostDecending = true;
                        break;
                    default:
                        bookings = bookings.OrderBy(b => b.TheGuests.GivenName);
                        break;
                }

                if (User.IsInRole("Guests"))
                {
                    Booking = await bookings
                                        .AsNoTracking()
                                        .Include(b => b.TheGuests)
                                        .Include(b => b.TheRooms)
                                        .Where(b => b.TheGuests.Email == User.Identity.Name)
                                        .ToListAsync();


                }
                else
                {
                    Booking = await bookings
                                        .AsNoTracking()
                                        .Include(b => b.TheGuests)
                                        .Include(b => b.TheRooms)
                                        .ToListAsync();
                }

                ViewData["NextCheckInDateOrder"] = sortOrder != "checkin_asc" ? "checkin_asc" : "checkin_desc";
                ViewData["NextCheckOutDateOrder"] = sortOrder != "checkout_asc" ? "checkout_asc" : "checkout_desc";
                ViewData["NextCostOrder"] = sortOrder != "cost_asc" ? "cost_asc" : "cost_desc";

            }
        }
    }
}
