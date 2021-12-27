using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("Sensor", Schema = "Organization")]
    public class Sensor
    {
        public int IX_Sensor { get; set; }

        public DateTime? EnteredDate { get; set; }

        [ForeignKey("IX_SensorType")]
        public SensorType? SensorType { get; set; }
        public int IX_SensorType { get; set; }

        [ForeignKey("IX_Node")]
        public Node? Node { get; set; }
        public int IX_Node { get; set; }

        public ICollection<SensorReading>? SensorReadings { get; set; }

    }
}