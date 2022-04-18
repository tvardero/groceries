namespace Groceries.Models;

public class Order
{
    public uint Id { get; set; }

    public IEnumerable<CartItem> Items { get; set; } = null!;

    public CustomerInfo CustomerInfo { get; set; } = null!;

    public decimal FixedSum { get; set; }

    public bool IsPaid { get; set; } = false;

    public bool IsDelivered { get; set; } = false;
}