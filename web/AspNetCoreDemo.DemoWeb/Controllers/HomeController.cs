using AspNetCoreDemo.DemoWeb.Models;
using AspNetCoreDemo.ShareWebFramework.Sessions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace AspNetCoreDemo.DemoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserSession _userSession;

        public HomeController(ILogger<HomeController> logger, IUserSession userSession)
        {
            _logger = logger;
            _userSession = userSession;
        }

        [Authorize]
        public IActionResult Index()
        {
            ViewBag.CurrentUserName = _userSession.DisplayName;
            return View();
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
