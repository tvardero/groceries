namespace Groceries.Models;

public class CartItem
{
    public Product Product { get; init; } = null!;

    public uint Quantity { get; set; }
}