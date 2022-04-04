using Microsoft.AspNetCore.Mvc;

namespace Groceries.Controllers;

public class CartController : Controller
{
    public CartController(Cart cart, IRepository<Product> productRepository)
    {
        _cart = cart;
        _productRepository = productRepository;
    }

    public IActionResult Index(string? returnUrl) => View(new CartViewModel() { Cart = _cart, ReturnUrl = returnUrl });

    [HttpPost]
    public IActionResult Add(AddToCartViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            CartItem? existingItem = _cart.Items.SingleOrDefault(ci => ci.Product.Id == model.ProductId);
            if (existingItem == null)
            {
                _cart.Items.Add(new()
                {
                    Product = _productRepository.Entities.Single(p => p.Id == model.ProductId),
                    Quantity = model.Quantity
                });
            }
            else
            {
                existingItem.Quantity += model.Quantity;
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        TempData["Message"] = "Something went wrong, try again.";
        TempData["MessageColor"] = "danger";
        return Redirect("/");
    }

    private readonly Cart _cart;
    private readonly IRepository<Product> _productRepository;
}