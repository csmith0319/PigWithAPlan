public class CategoryGroupViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int BudgetId { get; set; }

    public List<CategoryViewModel>? Categories { get; set; }
}
