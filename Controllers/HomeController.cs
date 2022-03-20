using Microsoft.AspNetCore.Mvc;

namespace Groceries.Controllers;

public class HomeController : Controller
{
    public HomeController(IRepository<Product> productRepository, IRepository<Category> categoryRepository)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public IActionResult Index()
    {
        return View();
    }

    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;
}
