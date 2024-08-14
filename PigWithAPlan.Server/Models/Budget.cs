using System.ComponentModel.DataAnnotations.Schema;

namespace PigWithAPlan.Server.Models
{
    public class Budget
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public required string Name { get; set; }
        public string? Color { get; set; } = string.Empty;
        public bool? Favorite { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public required virtual User User { get; set; }
    }
}