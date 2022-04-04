namespace Groceries.Models.ViewModels;

public class CartViewModel
{
    public Cart Cart { get; init; } = null!;

    public string? ReturnUrl { get; init; }
}