using System.ComponentModel.DataAnnotations.Schema;

namespace PigWithAPlan.Server.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [ForeignKey("Payee")]
        public int PayeeId { get; set; }
        [ForeignKey("Budget")]
        public required int BudgetId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
        public virtual Payee? Payee { get; set; }
        public required virtual Budget Budget { get; set; }
    }
}
