using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Models;
using InvoiceGenerator.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace InvoiceGenerator.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager, IUserService userService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userService = userService;
        }

        public IActionResult Index()
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost("/home/register")]
        public async Task<IActionResult> Register([FromBody] RegisterInputModel input)
        {
            await _userService.RegisterAsync(new RegisterModel
            {
                Email = input.Email,
                Address = input.Address,
                CompanyLogo = input.CompanyLogo,
                CompanyName = input.CompanyName,
                ContactNo = input.ContactNo,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Password = input.Password,
                UserName = input.UserName,
                VAT = input.VAT
            });

            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, true, false);

            if (result.Succeeded)
                return RedirectToAction(nameof(Index));
            return BadRequest("Invalid credentials. Please try again.");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost("/home/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, false);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));
            return BadRequest("Invalid credentials. Please try again.");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login)); // test, or add "Home", action, controller
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
