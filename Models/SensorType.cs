using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("SensorType", Schema = "Common")]
    [Index(nameof(SensorType.Name), IsUnique = true)]
    [Index(nameof(SensorType.DataType), IsUnique = true)]
    public class SensorType
    {
        public int IX_SensorType { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(280)]
        public string? Description { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(20)]
        public string DataType { get; set; }

        public DateTime? EnteredDate { get; set; }

        public ICollection<Sensor>? Sensors { get; set; }
    }
}