namespace Groceries.Repositories;

public class EFOrderRepository : IRepository<Order>
{
    public IQueryable<Order> Entities => _context.Orders;

    public EFOrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
        _context.SaveChanges();
    }

    private readonly ApplicationDbContext _context;
}