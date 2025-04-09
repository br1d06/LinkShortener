using System.Diagnostics;
using LinkShortener.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Controllers.Mvc
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
