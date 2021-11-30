using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GardenService.Models
{
    public class Moisture
    {

        public int IX_Moisture { get; set; }
        public float Value { get; set; }
        public DateTime? EnteredDate { get; set; }
    }
}