using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GardenService.Models;

namespace GardenService
{
    public class GardenServicesDbContext : DbContext
    {
        public DbSet<Moisture> Moistures { get; set; } = null!;
        public string? DbPath { get; private set; }

        public GardenServicesDbContext(DbContextOptions<GardenServicesDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Telemetry.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
            //=> options.UseSqlite($"Data Source={DbPath}");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moisture>(entity =>
            {
                entity.HasKey(o => new { o.IX_Moisture });
            });

            modelBuilder.Entity<Moisture>().HasData(
                new Moisture { IX_Moisture = 1, Value = 1.4f, EnteredDate = DateTime.Now },
                new Moisture { IX_Moisture = 2, Value = 1.5f, EnteredDate = DateTime.Now },
                new Moisture { IX_Moisture = 3, Value = 1.6f, EnteredDate = DateTime.Now }
            );

        }

    }
}