using InvoiceGenerator.Common.Exception;
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
        private readonly ICurrencyService _currencyService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, SignInManager<User> signInManager, IUserService userService, ICurrencyService currencyService)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userService = userService;
            _currencyService = currencyService;
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
            if (string.IsNullOrEmpty(input.UserName) || input.UserName == "undefined")
                throw new IGException("Username is not provided");

            if (string.IsNullOrEmpty(input.Email) || input.Email == "undefined")
                throw new IGException("Email is not provided");

            if (string.IsNullOrEmpty(input.Address) || input.Address == "undefined")
                throw new IGException("Address is not provided");

            if (string.IsNullOrEmpty(input.Password) || input.Password == "undefined")
                throw new IGException("Password is not provided");

            if (string.IsNullOrEmpty(input.FirstName) || input.FirstName == "undefined")
                throw new IGException("First name is not provided");

            if (string.IsNullOrEmpty(input.LastName) || input.LastName == "undefined")
                throw new IGException("Last name is not provided");

            if (string.IsNullOrEmpty(input.ContactNo) || input.ContactNo == "undefined")
                throw new IGException("Contact no is not provided");

            if (string.IsNullOrEmpty(input.CompanyName) || input.CompanyName == "undefined")
                throw new IGException("Company name is not provided");

            if (input.CurrencyId == 0)
                throw new IGException("Currency is not provided");

            if (input.Vat < 0 || input.Vat > 100)
                throw new IGException("Vat can only be between 0 and 100.");

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
                CurrencyId = input.CurrencyId,
                Vat = input.Vat,
                CompanyLogo = input.CompanyLogo
            });

            if (await _userService.SignInAsync(new SignInModel { Username = input.UserName, Password = input.Password }))
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

        [Authorize]
        [HttpGet]
        public IActionResult Edit() => View();

        [Authorize]
        [HttpGet("/api/user/getDetails")]
        public async Task<IActionResult> GetDetails()
        {
            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);
            var userDetails = await _userService.GetAsync(userId);
            return Ok(userDetails);
        }

        [Authorize]
        [HttpPost("/api/user/editDetails")]
        public async Task<IActionResult> EditDetails([FromForm] EditUserInputModel input)
        {
            if (string.IsNullOrEmpty(input.Email) || input.Email == "undefined")
                throw new IGException("Email is not provided");

            if (string.IsNullOrEmpty(input.Address) || input.Address == "undefined")
                throw new IGException("Address is not provided");

            if (string.IsNullOrEmpty(input.FirstName) || input.FirstName == "undefined")
                throw new IGException("First name is not provided");

            if (string.IsNullOrEmpty(input.LastName) || input.LastName == "undefined")
                throw new IGException("Last name is not provided");

            if (string.IsNullOrEmpty(input.ContactNo) || input.ContactNo == "undefined")
                throw new IGException("Contact no is not provided");

            if (string.IsNullOrEmpty(input.CompanyName) || input.CompanyName == "undefined")
                throw new IGException("Company name is not provided");

            if (input.CurrencyId == 0)
                throw new IGException("Currency is not provided");

            if (input.Vat < 0 || input.Vat > 100)
                throw new IGException("Vat can only be between 0 and 100.");

            long userId = long.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value);

            await _userService.UpdateAsync(new UpdateModel
            {
                Id = userId,
                FirstName = input.FirstName,
                CompanyName = input.CompanyName,
                LastName = input.LastName,
                Email = input.Email,
                ContactNo = input.ContactNo,
                Address = input.Address,
                Vat = input.Vat,
                CompanyLogo = input.CompanyLogo,
                CurrencyId = input.CurrencyId
            });

            return Ok();
        }

        [HttpGet("/api/user/getCurrencies")]
        public async Task<IActionResult> GetCurrencies()
        {
            var currencies = await _currencyService.GetAsync();
            return Ok(currencies);
        }
    }
}
