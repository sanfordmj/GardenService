using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("Sensor", Schema="Organization")]
    [Index(nameof(Sensor.Name), nameof(Sensor.IX_User), IsUnique = true)]
    public class Sensor
    {
        public int IX_Sensor { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string Name { get; set;}

        public DateTime? EnteredDate { get; set; }

        [ForeignKey("IX_User")]
        public User User { get; set;}
        public int IX_User { get; set; }

        [ForeignKey("IX_SensorType")]
        public SensorType SensorType { get; set;}
        public int IX_SensorType { get; set; }
        
    }
}