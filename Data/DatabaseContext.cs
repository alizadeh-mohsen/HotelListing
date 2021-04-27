using HotelListing.Data.model;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotel> Hotels { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>().HasData(
            new Country
            {
                Id = 1,
                Name = "Germany"
            },

            new Country
            {
                Id = 2,
                Name = "Spain"
            },
            new Country
            {
                Id = 3,
                Name = "Indonesia"
            },
            new Country
            {
                Id = 4,
                Name = "Brazil"
            }
            );

            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    CountryId = 1,
                    Name = "Berlin Hotel",
                    Rating = 7.5
                },
                new Hotel
                {
                    Id = 2,
                    CountryId = 1,
                    Name = "Hamburg Hotel",
                    Rating = 7.2
                }, new Hotel
                {
                    Id = 3,
                    CountryId = 2,
                    Name = "Barcelona Hotel",
                    Rating = 8.0
                }, new Hotel
                {
                    Id = 4,
                    CountryId = 2,
                    Name = "Ibiza Hotel",
                    Rating = 8.9
                }, new Hotel
                {
                    Id = 5,
                    CountryId = 3,
                    Name = "Bali Hotel",
                    Rating = 9.8
                },
                new Hotel
                {
                    Id = 6,
                    CountryId = 4,
                    Name = "Rio Hotel",
                    Rating = 9.5
                }

                );
        }
    }
}
