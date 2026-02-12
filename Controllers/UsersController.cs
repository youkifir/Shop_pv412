using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Shop_pv412.Controllers
{
    public class UsersController : Controller
    {
        public UserManager<IdentityUser> _userManager { get; set; }
        public RoleManager<IdentityRole> _roleManager { get; set; }
        public SignInManager<IdentityUser> _signInManager { get; set; }
        public UsersController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        //Get: Users/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //Post: Users/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind
            ("UserName,Email,PasswordHash")] IdentityUser newUser)
        {
            if (ModelState.IsValid)
            {
                newUser.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            return View();
        }
        //Get: Users/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        //Post: Users/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,PasswordHash")] IdentityUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, user.PasswordHash, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest("Error[01]");
                }
            }
            return BadRequest("Error[02]");
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole([Bind("Name")] IdentityRole newRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(newRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return BadRequest("Error");
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult AssignRole() => View();
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRole(string roleId, string userId)
        {
            if (string.IsNullOrEmpty(roleId) || string.IsNullOrEmpty(userId))
            {
                return BadRequest("Error[01]");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Error[02]");
            }
            var role = await _roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                return BadRequest("Error[03]");
            }
            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return BadRequest(result);
        }
    }
}
