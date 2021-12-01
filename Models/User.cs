using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace GardenService.Models
{
    public class User
    {
        public int IX_User { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set;}
        public string? FirstName { get; set;}
        public string? LastName { get; set;}
        public string? Email { get; set;}
        public string? Address { get; set;}
        public string? City { get; set;}
        public string? State { get; set;}
        public string? ZipCode {get;set;}            
        public DateTime? EnteredDate { get; set; }

        public ICollection<Moisture> Moistures { get; set; }
    }
}