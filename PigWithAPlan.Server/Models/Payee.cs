namespace PigWithAPlan.Server.Models
{
    public class Payee
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public int UpdatedBy { get; set; }
    }
}
