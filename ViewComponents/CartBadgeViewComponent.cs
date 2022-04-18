using Microsoft.AspNetCore.Mvc;

namespace Groceries.ViewComponents;

public class CartBadgeViewComponent : ViewComponent
{
    public CartBadgeViewComponent(Cart cart)
    {
        _cart = cart;
    }

    public IViewComponentResult Invoke()
    {
        return View(model: _cart.TotalSum);
    }

    private Cart _cart;
}