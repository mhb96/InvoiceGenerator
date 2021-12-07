using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Models;
using InvoiceGenerator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace InvoiceGenerator.Controllers
{
    public class UserController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, SignInManager<User> signInManager, IUserService userService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userService = userService;
        }

        [HttpGet("/user/login")]
        public IActionResult Login() => View();

        [HttpPost("/user/login")]
        public async Task<IActionResult> Login([FromBody] LoginModel input)
        {
            if (input.Password is null || input.Password is null)
                return BadRequest("Invalid credentials. Please try again.");

            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");
            return BadRequest("Invalid credentials. Please try again.");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login)); // test, or add "Home", action, controller
        }

        [HttpGet("/user/register")]
        public IActionResult Register() => View();

        [HttpPost("/user/register")]
        public async Task<IActionResult> Register([FromForm] RegisterInputModel input)
        {
            await _userService.RegisterAsync(new RegisterModel
            {
                UserName = input.UserName,
                Password = input.Password,
                FirstName = input.FirstName,
                CompanyName = input.CompanyName,
                LastName = input.LastName,
                Email = input.Email,
                ContactNo = input.ContactNo,
                Address = input.Address,
                Vat = input.Vat,
                CompanyLogo = input.CompanyLogo
            });

            var result = await _signInManager.PasswordSignInAsync(input.UserName, input.Password, true, false);

            if (result.Succeeded)
                return Ok();
            return BadRequest("Registration failed.");
        }
    }
}
