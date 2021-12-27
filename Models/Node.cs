using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GardenService.Models
{
    [Table("Node", Schema = "Organization")]
    [Index(nameof(Node.Name), nameof(Node.IX_User), IsUnique = true)]
    public class Node
    {
        public int IX_Node { get; set; }

        [Column(TypeName = "NVARCHAR")]
        [Required]
        [StringLength(80)]
        public string Name { get; set; }

        [ForeignKey("IX_User")]
        public User? User { get; set; }
        public int IX_User { get; set; }

        public DateTime? EnteredDate { get; set; }

        public ICollection<Sensor>? Sensors { get; set; }

    }

}