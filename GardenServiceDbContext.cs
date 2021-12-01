using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GardenService.Models;

namespace GardenService
{
    public class GardenServicesDbContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Sensor> Sensors { get; set; } = null!;
        public DbSet<SensorType> SensorTypes { get; set; } = null!;
        public DbSet<Moisture> Moistures { get; set; } = null!;
        public DbSet<Error> Errors { get; set; } = null!;
        public DbSet<Trace> Traces { get; set; } = null!;
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
            modelBuilder.Entity<User>().HasKey(o => new { o.IX_User }).HasMany(b => b.Moistures);

            modelBuilder.Entity<SensorType>().HasKey(o => new { o.IX_SensorType });

            modelBuilder.Entity<Sensor>().HasKey(o => new { o.IX_Sensor });

            modelBuilder.Entity<Moisture>().HasKey(o => new { o.IX_Moisture });  

            modelBuilder.Entity<Error>().HasKey(o => new { o.IX_Error });

            modelBuilder.Entity<Trace>().HasKey(o => new { o.IX_Trace });
            
            modelBuilder.Entity<User>().HasData(
                new User { IX_User = 1, UserName = "msanford", Password = "@Pqkrtsbm1", 
                                FirstName = "Michael", LastName = "Sanford", Email = "sanford.mj@gmail.com", 
                                Address = "24017 Heilum CT", City = "Forsest Lake", State = "MN", ZipCode = "55025", EnteredDate = DateTime.Now }
            );
            modelBuilder.Entity<SensorType>().HasData(
                new SensorType { IX_SensorType = 1, Name = "Unit Test", Description = "Unit Test", DataType = "float", EnteredDate = DateTime.Now }
            );
            modelBuilder.Entity<Sensor>().HasData(
                new Sensor { IX_Sensor = 1, IX_SensorType = 1, Name = "Unit Test", EnteredDate = DateTime.Now }
            );
            modelBuilder.Entity<Moisture>().HasData(
                new Moisture { IX_Moisture = 1, IX_User = 1, Value = 1.4f, EnteredDate = DateTime.Now },
                new Moisture { IX_Moisture = 2, IX_User = 1, Value = 1.5f, EnteredDate = DateTime.Now },
                new Moisture { IX_Moisture = 3, IX_User = 1, Value = 1.6f, EnteredDate = DateTime.Now }
            );
            modelBuilder.Entity<Error>().HasData(
                new Error {IX_Error = 1, Level = "Unit Test", Logger = "Unit Test", Message = "Unit Test", StackTrace = "Unit Test", CreateDate = DateTime.Now},
                new Error {IX_Error = 2, Level = "Unit Test1", Logger = "Unit Test1", Message = "Unit Test1", StackTrace = "Unit Test1", CreateDate = DateTime.Now},
                new Error {IX_Error = 3, Level = "Unit Test2", Logger = "Unit Test2", Message = "Unit Test2", StackTrace = "Unit Test2", CreateDate = DateTime.Now}
            );
            modelBuilder.Entity<Trace>().HasData(
                new Trace {IX_Trace = 1, Level = "Unit Test", Logger = "Unit Test", Message = "Unit Test", CreateDate = DateTime.Now},
                new Trace {IX_Trace = 2, Level = "Unit Test1", Logger = "Unit Test1", Message = "Unit Test1", CreateDate = DateTime.Now},
                new Trace {IX_Trace = 3, Level = "Unit Test2", Logger = "Unit Test2", Message = "Unit Test2", CreateDate = DateTime.Now}
            );
        }

    }
}