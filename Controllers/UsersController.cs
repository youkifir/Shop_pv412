using Microsoft.AspNetCore.Mvc;

namespace Shop_pv412.Controllers
{
    public class UsersController : Controller
    {   //Create GET methods for test VIEWS
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AssignRole()
        {
            return View();
        }
    }
}
