using System.ComponentModel.DataAnnotations;

namespace Groceries.Models;

public class Cart
{
    public List<CartItem> Items { get; } = new();

    public decimal TotalSum => Items.Sum(ci => ci.Product.Price * ci.Quantity);
}