namespace Groceries.Repositories;

public class EFCategoryRepository : IRepository<Category>
{
    public IQueryable<Category> Entities => _context.Categories;

    public EFCategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Category category)
    {
        _context.Categories.Add(category);
        _context.SaveChanges();
    }

    public void Update(Category category)
    {
        _context.Categories.Update(category);
        _context.SaveChanges();
    }

    public void Remove(Category category)
    {
        _context.Categories.Remove(category);
        _context.SaveChanges();
    }

    private readonly ApplicationDbContext _context;
}