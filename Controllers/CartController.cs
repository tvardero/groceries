using Microsoft.AspNetCore.Mvc;

namespace Groceries.Controllers;

public class CartController : Controller
{
    public CartController(Cart cart, IRepository<Product> productRepository, IRepository<Order> orderRepository)
    {
        _cart = cart;
        _productRepository = productRepository;
        _orderRepository = orderRepository;
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

        TempData["Message"] = "Щось пішло не так, спробуйте ще раз.";
        TempData["MessageColor"] = "danger";
        return Redirect(returnUrl ?? "/");
    }

    [HttpPost]
    public IActionResult Update(AddToCartViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            CartItem item = _cart.Items.Single(ci => ci.Product.Id == model.ProductId);
            item.Quantity = model.Quantity;
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        TempData["Message"] = "Щось пішло не так, спробуйте ще раз.";
        TempData["MessageColor"] = "danger";
        return RedirectToAction(nameof(Index), new { returnUrl });
    }

    [HttpPost]
    public IActionResult Delete(AddToCartViewModel model, string? returnUrl)
    {
        if (ModelState.IsValid)
        {
            CartItem item = _cart.Items.Single(ci => ci.Product.Id == model.ProductId);
            _cart.Items.Remove(item);
            return RedirectToAction(nameof(Index), new { returnUrl });
        }

        TempData["Message"] = "Щось пішло не так, спробуйте ще раз.";
        TempData["MessageColor"] = "danger";
        return RedirectToAction(nameof(Index), new { returnUrl });
    }

    public IActionResult Checkout(string? returnUrl = null)
    {
        if (_cart.TotalSum > 0)
        {
            return View(new CheckoutViewModel() { ReturnUrl = returnUrl });
        }

        TempData["Message"] = "Сума покупки повинна бути більшою за нуль.";
        TempData["MessageColor"] = "danger";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public IActionResult Checkout(CheckoutViewModel model)
    {
        if (ModelState.IsValid)
        {
            _cart.Items.Clear();
            return RedirectToAction(nameof(Success));
        }
        return View(model);
    }

    public IActionResult Success() => View();

    private readonly Cart _cart;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Order> _orderRepository;
}