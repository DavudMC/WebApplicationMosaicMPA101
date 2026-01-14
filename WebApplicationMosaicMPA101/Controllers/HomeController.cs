using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;


namespace WebApplicationMosaicMPA101.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
