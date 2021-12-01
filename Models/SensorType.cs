using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GardenService.Models
{
    public class SensorType
    {
        public int IX_SensorType { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set;}
        public string? DataType { get; set;}
        public DateTime? EnteredDate { get; set; }

    }
}