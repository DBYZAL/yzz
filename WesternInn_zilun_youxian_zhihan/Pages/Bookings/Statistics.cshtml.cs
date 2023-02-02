using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_zilun_youxian_zhihan.Data;
using WesternInn_zilun_youxian_zhihan.ViewModels;

namespace WesternInn_zilun_youxian_zhihan.Pages.Bookings
{
    [Authorize(Roles = "Administrator")]

    public class StatisticsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IList<GuestDistributionViewModel> GuestDistribution { get; set; }
        public IList<BookingDistributionViewModel> BookingDistribution { get; set; }

        public StatisticsModel(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task OnGetAsync()
        {
            GuestDistribution = await _context.Guest
                                        .GroupBy(g => g.PostCode)
                                        .OrderByDescending(g => g.Count())
                                        .Select(g => new GuestDistributionViewModel { PostCode = g.Key, NumberOfGuests = g.Count() })
                                        .ToListAsync();

            BookingDistribution = await _context.Booking
                                        .GroupBy(g => g.RoomID)
                                        .OrderByDescending(g => g.Count())
                                        .Select(g => new BookingDistributionViewModel { RoomID = g.Key, NumberOfBookings = g.Count() })
                                        .ToListAsync();
        }
    }
}
