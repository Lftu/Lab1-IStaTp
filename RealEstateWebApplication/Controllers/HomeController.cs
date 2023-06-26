using Microsoft.AspNetCore.Mvc;
using RealEstateWebApplication.Models;
using System.Diagnostics;

namespace RealEstateWebApplication.Controllers
{
    // The HomeController is responsible for handling requests related to the home page and privacy page of a real estate web application.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructs a new instance of the HomeController class.
        // The constructor takes a logger as a parameter, which is used for logging purposes.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Handles the request for the home page.
        // Returns a ViewResult, which renders the "Index" view.
        public IActionResult Index()
        {
            return View();
        }

        // Handles the request for the privacy page.
        // Returns a ViewResult, which renders the "Privacy" view.
        public IActionResult Privacy()
        {
            return View();
        }

        // Handles the request for an error page.
        // Sets the ResponseCache attribute to disable caching.
        // Returns a ViewResult, which renders the "Error" view, passing an ErrorViewModel object containing error details.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}