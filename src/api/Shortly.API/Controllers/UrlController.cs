﻿using Microsoft.AspNetCore.Mvc;

namespace Shortly.API.Controllers
{
    public class UrlController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
