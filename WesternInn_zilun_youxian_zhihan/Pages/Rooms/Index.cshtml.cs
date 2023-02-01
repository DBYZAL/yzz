using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WesternInn_zilun_youxian_zhihan.Data;
using WesternInn_zilun_youxian_zhihan.Models;

namespace WesternInn_zilun_youxian_zhihan.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext _context;

        public IndexModel(WesternInn_zilun_youxian_zhihan.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Room { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public int SearchInt { get; set; } = int.Empty;
        public async Task OnGetAsync()
        {
            var rooms = (IQueryable<Room>)_context.Room;

            if (!int.IsNullOrEmpty(SearchInt))
            {
                rooms = rooms.Where(s => s.BedCount.Contains(SearchInt));
            }

            if (rooms != null)
            {
                Room = await rooms.ToListAsync();
            }
        }


        public string SearchString { get; set; } = string.Empty;

    }
}
