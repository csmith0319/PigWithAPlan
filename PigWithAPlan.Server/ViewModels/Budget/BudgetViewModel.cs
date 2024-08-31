public class BudgetViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Color { get; set; }
    public bool? Favorite { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int? UserId { get; set; }
    public string? UserName { get; set; } = string.Empty;

}
