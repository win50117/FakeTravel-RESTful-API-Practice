using FakeTravel.Models;
using Microsoft.EntityFrameworkCore;
namespace FakeTravel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<TravelRoute> TravelRoutes { get; set; }
        public DbSet<TravelRoutePicture> TravelRoutePictures { get; set; }
    }
}