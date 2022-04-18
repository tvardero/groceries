namespace Groceries.Models;

public class Cart
{
    public IEnumerable<CartItem> Entities => items;

    public decimal TotalSum => items.Sum(ci => ci.Product.Price * ci.Quantity);

    public virtual void Add(CartItem item) => items.Add(item);

    public virtual void Remove(CartItem item) => items.Remove(item);

    public virtual void Clear() => items.Clear();

    protected List<CartItem> items = new();
}