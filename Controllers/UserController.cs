using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using GameTracker.Data.Entities;
using GameTracker.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using static GameTracker.Data.DataConstants.ControllerConstants;
using Ganss.Xss;
using static GameTracker.Data.DataConstants;
using GameTracker.Data;

namespace GameTracker.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            RegisterViewModel registerViewModel = new RegisterViewModel();
            return View(registerViewModel);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            User newUser = new User()
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction(nameof(Login));
            }

            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(registerViewModel);

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("Index", "Home");
            }
            LoginViewModel loginViewModel = new LoginViewModel();
            return View(loginViewModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }
            User user = await _userManager.FindByNameAsync(loginViewModel.Username);

            if (user != null)
            {
                loginViewModel.Password = loginViewModel.Password.Replace("[", "[[]");
                loginViewModel.Password = loginViewModel.Password.Replace("%", "[%]");
                loginViewModel.Password = loginViewModel.Password.Replace("_", "[_]");
                var sanitizer = new HtmlSanitizer();
                SignInResult result = await _signInManager.PasswordSignInAsync(user, sanitizer.Sanitize(loginViewModel.Password), false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError(string.Empty, GeneralErrorMessage);

            return View(loginViewModel);

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> CreateRoles()
        {

            await _roleManager.CreateAsync(new IdentityRole(DataConstants.RoleContants.Admin));

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AddUsersToRoles()
        {
            string email = "nikola220@abv.bg";
            var user4 = await _userManager.FindByEmailAsync(email);

            await _userManager.AddToRolesAsync(user4, new string[] { DataConstants.RoleContants.Admin});

            return RedirectToAction("Index", "Home");
        }
    }
}
