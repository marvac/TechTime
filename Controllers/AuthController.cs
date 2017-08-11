using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using TechTime.Models;
using TechTime.ViewModels;

namespace TechTime.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<UserLogin> _signInManager;
        private readonly UserManager<UserLogin> _userManager;
        private ILogger<AuthController> _logger;

        public AuthController(SignInManager<UserLogin> signInManager, UserManager<UserLogin> userManager, ILogger<AuthController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel viewModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(
                    viewModel.Username,
                    viewModel.Password,
                    isPersistent: true,
                    lockoutOnFailure: false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Index", "Home");
                    else
                        return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "Username or password incorrect");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                UserLogin user = new UserLogin()
                {
                    UserName = viewModel.Username,
                    Email = viewModel.Email,
                    Name = viewModel.Username
                };

                var result = await _userManager.CreateAsync(user, viewModel.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent:true);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogDebug(error.Description);
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }

            return View();
        }

        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
            }
                
            return RedirectToAction("Login", "Auth");
        }
    }
}
