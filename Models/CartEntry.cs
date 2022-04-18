using System.ComponentModel.DataAnnotations.Schema;

namespace Groceries.Models;

public class CartItem
{
    public uint OrderId { get; set; }

    public uint ProductId { get; set; }

    [ForeignKey(nameof(OrderId))]
    public Order Order { get; set; } = null!;

    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; } = null!;

    public uint Quantity { get; set; }
}