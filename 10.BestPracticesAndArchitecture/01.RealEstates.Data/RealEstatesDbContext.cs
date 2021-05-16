using _02.RealEstates.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace _01.RealEstates.Data
{
    public class RealEstatesDbContext : DbContext
    {
        public RealEstatesDbContext()
        {
        }

        public RealEstatesDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Building> Buildings { get; set; }

        public DbSet<BuildingType> BuildingTypes { get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyTag> PropertiesTags { get; set; } 

        public DbSet<PropertyType> PropertyTypes { get; set; }

        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PropertyTag>()
                .HasKey(x => new { x.PropertyId, x.TagId });
        }
    }
}
