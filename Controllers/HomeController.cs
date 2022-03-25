using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Groceries.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
