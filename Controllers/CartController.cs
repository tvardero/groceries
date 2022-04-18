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
            CartItem? existingItem = _cart.Entities.SingleOrDefault(ci => ci.Product.Id == model.ProductId);
            if (existingItem == null)
            {
                _cart.Add(new()
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
            CartItem item = _cart.Entities.Single(ci => ci.Product.Id == model.ProductId);
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
            CartItem item = _cart.Entities.Single(ci => ci.Product.Id == model.ProductId);
            _cart.Remove(item);
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
            Order order = new()
            {
                Items = _cart.Entities,
                CustomerInfo = model.CustomerInfo,
                FixedSum = _cart.TotalSum
            };

            // Resolve tracking of CartItem.Product entities
            foreach (CartItem item in order.Items)
            {
                item.Product = _productRepository.Entities.Single(p => p.Id == item.Product.Id);
            }

            _orderRepository.Add(order);

            _cart.Clear();

            return RedirectToAction(nameof(Payment), new { orderId = order.Id });
        }

        return View(model);
    }

    public IActionResult Payment(uint orderId = 0)
    {
        Order? order;

        if (orderId != 0)
        {
            order = _orderRepository.Entities.SingleOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                TempData["Message"] = "Не знайдено замовлення з id " + orderId;
                TempData["MessageColor"] = "danger";
                return Redirect("/");
            }
        }
        else
        {
            TempData["Message"] = "Не знайдено замовлення з id " + orderId;
            TempData["MessageColor"] = "danger";
            return Redirect("/");
        }

        if (order.IsPaid)
        {
            return RedirectToAction(nameof(Success));
        }

        PaymentViewModel model = new() { OrderId = order.Id };
        return View(model);
    }

    [HttpPost]
    public IActionResult Payment(PaymentViewModel model)
    {
        if (ModelState.IsValid)
        {
            Order? order = _orderRepository.Entities.SingleOrDefault(o => o.Id == model.OrderId);
            if (order == null)
            {
                ModelState.AddModelError("", "Не знайдено замовлення, спробуйте ще раз.");
                return View(model);
            }

            order.IsPaid = true;
            _orderRepository.Update(order);

            return RedirectToAction(nameof(Success));
        }

        return View(model);
    }

    public IActionResult Success() => View();

    private readonly Cart _cart;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Order> _orderRepository;
}