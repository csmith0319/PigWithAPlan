public class TransactionDetailViewModel
{
    public int TransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string? PayeeName { get; set; }
    public string? BudgetName { get; set; }
    public string? BudgetColor { get; set; }
    public int UserId { get; set; }
    public string? UserName { get; set; }
}
