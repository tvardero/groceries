﻿using Microsoft.AspNetCore.Mvc;

namespace Groceries.Controllers;

public class HomeController : Controller
{
    public IActionResult Index() => View();
}
