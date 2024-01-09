using AppBayPageMvc.Models;
using AppBayPageMvc.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AppBayPageMvc.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVm registerVm)
        {
            AppUser appUser = new AppUser()
            {
                Email = registerVm.Email,
                Name = registerVm.Name,
                Surname = registerVm.Surname,
                UserName=registerVm.Username,
                
            };

            await _userManager.CreateAsync(appUser, registerVm.Password);
            await _signInManager.SignInAsync(appUser, false);
            return RedirectToAction("Index","Home");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVm loginVm)
        {
            AppUser user =await _userManager.FindByNameAsync(loginVm.Username);
            if(user is not null)
            {
                await _signInManager.SignInAsync(user, false);
            }
            return RedirectToAction("Index","Home");
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }
    }
}
