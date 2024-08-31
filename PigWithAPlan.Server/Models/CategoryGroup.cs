using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PigWithAPlan.Server.Models
{
    public class CategoryGroup
    {
        public int Id { get; set; }

        [ForeignKey("Budget")]
        public int BudgetId { get; set; }

        [Required]
        public required string Name { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }

        public virtual Budget? Budget { get; set; }

        public virtual List<Category>? Category { get; set; }
    }
}