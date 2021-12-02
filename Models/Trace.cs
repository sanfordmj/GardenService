using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("Trace", Schema="Logging")]
    public class Trace
    {
        public int IX_Trace { get; set; }
        public string? Level {get; set;}
        public string? Logger { get; set;}
        public string? Message { get; set;}
        public DateTime? CreateDate { get; set; }
    }
}