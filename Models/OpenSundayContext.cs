using Microsoft.EntityFrameworkCore;

namespace OpenSundayApi.Models
{
    public class OpenSundayContext : DbContext
    {
        public OpenSundayContext(DbContextOptions<OpenSundayContext> options) : base(options) { }

        public DbSet<Location> Location { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<City> City { get; set; }

        public DbSet<Like> Like { get; set; }
        //public DbSet<Role> Role { get; set; }
        public DbSet<User> User { get; set; }


protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Like>().HasKey(ba => new { ba.FK_Location, ba.FK_User });      
}



    }
}