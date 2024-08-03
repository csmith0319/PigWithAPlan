public class BudgetViewDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Color { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; } = string.Empty;
    
}
