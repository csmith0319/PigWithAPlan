public class BudgetTransactionViewDTO
{
    public int TransactionId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? PayeeId { get; set; }
    public string? PayeeName { get; set; }
    
}
