using Microsoft.AspNetCore.Mvc;

namespace Shop_pv412.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
