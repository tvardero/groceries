using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Groceries.Controllers;

public class ProductsController : Controller
{
    public ProductsController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index(uint categoryId = 0)
    {
        var model = _productRepository.Entities.Include(pr => pr.Category);

        if (categoryId != 0)
        {
            Category? category = _categoryRepository.Entities.SingleOrDefault(ct => ct.Id == categoryId);
            return category == null
                ? BadRequest()
                : View(model.Where(pr => pr.Category == category));
        }

        return View(model);
    }

    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;

}