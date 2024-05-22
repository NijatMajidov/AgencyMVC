using AgencyMVC.Business.Services.Abstract;
using AgencyMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AgencyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPortfolioService _portfolioService;

        public HomeController(ILogger<HomeController> logger,IPortfolioService portfolioService)
        {
            _logger = logger;
            _portfolioService = portfolioService;
        }

        public IActionResult Index()
        {
            return View(_portfolioService.GetAllPortfolio());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}