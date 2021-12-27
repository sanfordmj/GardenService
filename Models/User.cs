using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("User", Schema = "Security")]
    [Index(nameof(User.Email), IsUnique = true)]
    public class User
    {

        public int IX_User { get; set; }

        [ForeignKey("IX_UserStatus")]
        public UserStatus? UserStatus { get; set; }
        public int IX_UserStatus { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string UserName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string Password { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string FirstName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string LastName { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(150)]
        public string Email { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(150)]
        public string? Address { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(150)]
        public string? City { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(150)]
        [Required]
        public string State { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [StringLength(50)]
        public string? ZipCode { get; set; }

        public DateTime? EnteredDate { get; set; }

        public ICollection<Node>? Nodes { get; set; }
    }
}