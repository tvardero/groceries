using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Groceries.Controllers;

public class ProductsController : Controller
{
    readonly uint ItemsPerPage = 12;

    public ProductsController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index(uint categoryId = 0, uint page = 1)
    {
        IEnumerable<Product> products = _productRepository.Entities.Include(pr => pr.Category);

        if (categoryId != 0)
        {
            Category? category = _categoryRepository.Entities.SingleOrDefault(ct => ct.Id == categoryId);
            if (category == null) return RedirectToAction(nameof(Index));

            products = products.Where(p => p.Category == category);
        }

        uint totalItems = (uint)products.Count();
        products = products.Skip((int)(ItemsPerPage * (page - 1))).Take((int)ItemsPerPage);

        if (!products.Any()) return RedirectToAction(nameof(Index), new { categoryId });

        PaginatedEnumerableViewModel<Product> model = new(products, new()
        {
            ItemsPerPage = ItemsPerPage,
            TotalItems = totalItems,
            CurrentPage = page
        });

        return View(model);
    }

    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;

}