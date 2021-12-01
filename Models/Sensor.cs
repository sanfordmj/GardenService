using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GardenService.Models
{
    public class Sensor
    {
        public int IX_Sensor { get; set; }
        public int? IX_SensorType { get; set; }
        public string? Name { get; set;}
        public DateTime? EnteredDate { get; set; }

    }
}