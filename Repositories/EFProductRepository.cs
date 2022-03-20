namespace Groceries.Repositories;

public class EFProductRepository : IRepository<Product>
{
    public IQueryable<Product> Entities => _context.Products;

    public EFProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Product product)
    {
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void Update(Product product)
    {
        _context.Products.Update(product);
        _context.SaveChanges();
    }

    public void Remove(Product product)
    {
        _context.Products.Remove(product);
        _context.SaveChanges();
    }

    private readonly ApplicationDbContext _context;
}