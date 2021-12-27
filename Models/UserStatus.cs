using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("UserStatus", Schema = "Common")]
    [Index(nameof(UserStatus.Name), IsUnique = true)]
    public class UserStatus
    {
        public int IX_UserStatus { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}