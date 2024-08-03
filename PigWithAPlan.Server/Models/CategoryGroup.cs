using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PigWithAPlan.Server.Models
{
    public class CategoryGroup
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
    }
}