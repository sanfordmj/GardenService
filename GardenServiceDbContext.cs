using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GardenService.Models;

namespace GardenService
{
    public class GardenServicesDbContext : DbContext
    {
        public DbSet<UserStatus> UserStatus { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Node> Nodes { get; set; } = null!;
        public DbSet<Sensor> Sensors { get; set; } = null!;
        public DbSet<SensorType> SensorTypes { get; set; } = null!;
        public DbSet<SensorReading> SensorReadings { get; set; } = null!;
        public DbSet<Error> Errors { get; set; } = null!;
        public DbSet<Trace> Traces { get; set; } = null!;
        public string? DbPath { get; private set; }

        public GardenServicesDbContext(DbContextOptions<GardenServicesDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreated();
            //Overridden db path for imbeded. ie. sqlite
            //var folder = Environment.SpecialFolder.LocalApplicationData;
            //var path = Environment.GetFolderPath(folder);
            //DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}Telemetry.db";
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //=> options.UseSqlite($"Data Source={DbPath}");

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        // => options.UseLazyLoadingProxies();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<UserStatus>().HasKey(o => new { o.IX_UserStatus });

            modelBuilder.Entity<User>().HasKey(o => new { o.IX_User });

            modelBuilder.Entity<SensorType>().HasKey(o => new { o.IX_SensorType });

            modelBuilder.Entity<Node>().HasKey(o => new { o.IX_Node });

            modelBuilder.Entity<Sensor>().HasKey(o => new { o.IX_Sensor });

            modelBuilder.Entity<SensorReading>().HasKey(o => new { o.IX_SensorReading });

            modelBuilder.Entity<Error>().HasKey(o => new { o.IX_Error });

            modelBuilder.Entity<Trace>().HasKey(o => new { o.IX_Trace });

            modelBuilder.Entity<UserStatus>().HasData(
                new UserStatus { IX_UserStatus = 1, Name = "Unit Test" }
            );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    IX_User = 1,
                    IX_UserStatus = 1,
                    UserName = "msanford",
                    Password = "@Pqkrtsbm1",
                    FirstName = "Michael",
                    LastName = "Sanford",
                    Email = "sanford.mj@gmail.com",
                    Address = "24017 Heilum CT",
                    City = "Forsest Lake",
                    State = "MN",
                    ZipCode = "55025",
                    EnteredDate = DateTime.Now
                }
            );


            modelBuilder.Entity<SensorType>().HasData(
                new SensorType { IX_SensorType = 1, Name = "Unit Test", Description = "Unit Test", DataType = "float", EnteredDate = DateTime.Now }
            );

            modelBuilder.Entity<Node>().HasData(
                new Node { IX_Node = 1, IX_User = 1, Name = "Unit Test", EnteredDate = DateTime.Now }
            );

            modelBuilder.Entity<Sensor>().HasData(
                new Sensor { IX_Sensor = 1, IX_SensorType = 1, IX_Node = 1, EnteredDate = DateTime.Now }
            );
            modelBuilder.Entity<SensorReading>().HasData(
                new SensorReading { IX_SensorReading = 1, IX_Sensor = 1, Value = 1.4f, EnteredDate = DateTime.Now },
                new SensorReading { IX_SensorReading = 2, IX_Sensor = 1, Value = 1.5f, EnteredDate = DateTime.Now },
                new SensorReading { IX_SensorReading = 3, IX_Sensor = 1, Value = 1.6f, EnteredDate = DateTime.Now }
            );
            modelBuilder.Entity<Error>().HasData(
                new Error { IX_Error = 1, Level = "Unit Test", Logger = "Unit Test", Message = "Unit Test", StackTrace = "Unit Test", CreateDate = DateTime.Now },
                new Error { IX_Error = 2, Level = "Unit Test1", Logger = "Unit Test1", Message = "Unit Test1", StackTrace = "Unit Test1", CreateDate = DateTime.Now },
                new Error { IX_Error = 3, Level = "Unit Test2", Logger = "Unit Test2", Message = "Unit Test2", StackTrace = "Unit Test2", CreateDate = DateTime.Now }
            );
            modelBuilder.Entity<Trace>().HasData(
                new Trace { IX_Trace = 1, Level = "Unit Test", Logger = "Unit Test", Message = "Unit Test", CreateDate = DateTime.Now },
                new Trace { IX_Trace = 2, Level = "Unit Test1", Logger = "Unit Test1", Message = "Unit Test1", CreateDate = DateTime.Now },
                new Trace { IX_Trace = 3, Level = "Unit Test2", Logger = "Unit Test2", Message = "Unit Test2", CreateDate = DateTime.Now }
            );
        }

    }
}