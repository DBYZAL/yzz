using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WesternInn_zilun_youxian_zhihan.Models;

namespace WesternInn_zilun_youxian_zhihan.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<WesternInn_zilun_youxian_zhihan.Models.Room> Room { get; set; } = default!;
        public DbSet<WesternInn_zilun_youxian_zhihan.Models.Guest> Guest { get; set; } = default!;
        public DbSet<WesternInn_zilun_youxian_zhihan.Models.Booking> Booking { get; set; } = default!;
    }
}