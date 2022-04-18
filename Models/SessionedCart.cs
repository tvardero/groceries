namespace Groceries.Models;

public class SessionedCart : Cart
{
    public SessionedCart(IHttpContextAccessor httpContextAccessor, IRepository<Product> productRepository)
    {
        _session = httpContextAccessor.HttpContext!.Session;
        _productRepository = productRepository;
        GetItemsFromSession();
    }

    public override void Add(CartItem item)
    {
        base.Add(item);
        WriteItemsToSession(items);
    }

    public override void Remove(CartItem item)
    {
        base.Remove(item);
        WriteItemsToSession(items);
    }

    public override void Clear()
    {
        base.Clear();
        WriteItemsToSession(items);
    }

    void GetItemsFromSession()
    {
        List<CartItem> result = new();

        string? data = _session.GetString("cart");

        if (!string.IsNullOrEmpty(data))
        {
            foreach (string itemString in data.Split(";"))
            {
                string[] values = itemString.Split(",");
                Product product = _productRepository.Entities.Single(p => p.Id == uint.Parse(values[0]));
                CartItem item = new() { Product = product, Quantity = uint.Parse(values[1]) };

                result.Add(item);
            }
        }

        items = result;
    }

    void WriteItemsToSession(IEnumerable<CartItem> items)
    {
        List<string> itemStrings = new();

        foreach (CartItem item in items)
        {
            itemStrings.Add($"{item.Product.Id},{item.Quantity}");
        }

        _session.SetString("cart", string.Join(";", itemStrings));
    }

    private readonly ISession _session;

    private readonly IRepository<Product> _productRepository;
}