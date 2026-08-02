using Microsoft.AspNetCore.Mvc;

namespace OSTech.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
