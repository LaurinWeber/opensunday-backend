using Microsoft.EntityFrameworkCore;

namespace OpenSundayApi.Models
{
    public class OpenSundayContext : DbContext
    {
        public OpenSundayContext(DbContextOptions<OpenSundayContext> options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }






    }
}