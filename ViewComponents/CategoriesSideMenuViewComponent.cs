using Microsoft.AspNetCore.Mvc;

namespace Groceries.ViewComponents;

public class CategoriesSideMenuViewComponent : ViewComponent
{
    public CategoriesSideMenuViewComponent(IRepository<Category> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public IViewComponentResult Invoke()
    {
        IEnumerable<Category> categories = _categoryRepository.Entities.OrderBy(c => c.Id);
        return View(categories);
    }

    private readonly IRepository<Category> _categoryRepository;
}