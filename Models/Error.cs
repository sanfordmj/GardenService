using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GardenService.Models
{
    public class Error
    {
        public int IX_Error { get; set; }
        public string? Level {get; set;}
        public string? Logger { get; set;}
        public string? Message { get; set;}
        public string? StackTrace { get; set;}
        public DateTime? CreateDate { get; set; }
    }
}