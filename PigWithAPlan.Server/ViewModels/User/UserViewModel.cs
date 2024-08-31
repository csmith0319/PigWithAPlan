using System.ComponentModel.DataAnnotations;

public class UserViewModel
{
    [Required]
    public required string Username { get; init; }
}