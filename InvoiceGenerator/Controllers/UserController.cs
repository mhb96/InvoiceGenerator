using InvoiceGenerator.Entities;
using InvoiceGenerator.Models;
using InvoiceGenerator.Services;
using InvoiceGenerator.Services.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
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
            if (input.Password is null || input.Password is null || !await _userService.SignInAsync(new SignInModel { Username = input.Username, Password = input.Password }))
                return BadRequest("Invalid credentials. Please try again.");
            return Ok();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
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

            if (await _userService.SignInAsync(new SignInModel { Username = input.UserName, Password = input.Password}))
                return Ok();
            return BadRequest("Registration failed.");
        }

        [Authorize]
        [HttpGet("/api/user/getUserInvoiceDetails")]
        public async Task<IActionResult> GetDetailsForInvoice()
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            var userDetails = await _userService.GetDetailsForInvoice(userId);
            return Ok(userDetails);
        }
    }
}
