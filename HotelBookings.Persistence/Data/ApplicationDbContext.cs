using HotelBookings.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HotelBookings.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Villa>().HasData(
                 new Villa { Id = 1, Name = "Fortune of time", ImageUrl = "https://placehold.co/600x401", Description = "Lorem Ipsum",  Occupancy = 4, Price = 90, Sqft = 550  },
                 new Villa { Id = 2, Name = "Dark Skies", ImageUrl = "https://placehold.co/600x401", Description = "Lorem Ipsum", Occupancy = 4, Price = 120, Sqft = 550 },
                 new Villa { Id = 3, Name = "Fortune of time", ImageUrl = "https://placehold.co/600x401", Description = "Lorem Ipsum", Occupancy = 4, Price = 210, Sqft = 550 });

            modelBuilder.Entity<VillaNumber>().HasData(
                 new VillaNumber { VillaId = 1, Villa_Number = 101 },
                 new VillaNumber { VillaId = 1, Villa_Number = 102 },
                 new VillaNumber { VillaId = 1, Villa_Number = 103 },
                 new VillaNumber { VillaId = 2, Villa_Number = 201 },
                 new VillaNumber { VillaId = 2, Villa_Number = 202 },
                 new VillaNumber { VillaId = 2, Villa_Number = 203 },
                 new VillaNumber { VillaId = 3, Villa_Number = 301 },
                 new VillaNumber { VillaId = 3, Villa_Number = 302 });
        }
    }
}
