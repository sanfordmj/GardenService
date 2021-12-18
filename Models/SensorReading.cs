using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("SensorReading", Schema = "Organization")]
    public class SensorReading
    {
        public int IX_SensorReading { get; set; }

        [Required]
        public float Value { get; set; }

        public DateTime? EnteredDate { get; set; }

        [ForeignKey("IX_Sensor")]
        public Sensor? Sensor { get; set; }
        public int IX_Sensor { get; set; }

        public ICollection<Sensor>? Sensors { get; set; }
    }
}