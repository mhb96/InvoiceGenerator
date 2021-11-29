using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Models;
using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, ILogger<BaseService> logger, UserManager<User> userManager) : base(unitOfWork, logger)
        {
            _userManager = userManager;
        }

        public async Task<UserModel> GetAsync(string username) =>
            await UnitOfWork.Query<User>(u => u.UserName == username).Select(u => new UserModel { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }).FirstOrDefaultAsync();

        public async Task<UserModel> GetAsync(long id) =>
            await UnitOfWork.Query<User>(u => u.Id == id).Select(u => new UserModel { Id = u.Id, Name = $"{u.FirstName} {u.LastName}" }).FirstOrDefaultAsync();
        
        public async Task RegisterAsync(RegisterModel input)
        {
            Logger.LogInformation($"Create user with email `{input.Email}` for application");
            var user = new User
            {
                Email = input.Email,
                NormalizedEmail = input.Email.ToUpper(),
                BusinessEmail = input.Email,
                UserName = input.UserName,
                NormalizedUserName = input.UserName.ToUpper(),
                AccessFailedCount = 0,
                EmailConfirmed = true,
                FirstName = input.FirstName,
                LastName = input.LastName,
                CompanyName = input.CompanyName,
                ContactNo = input.ContactNo,
                Address = input.Address,
                VAT = input.VAT,
                CompanyLogo = input.CompanyLogo,
                LockoutEnabled = false, 
            };

            var result = await _userManager.CreateAsync(user, input.Password);
            if (result.Succeeded)
            {
                Logger.LogInformation($"Created user `{user.Email}` successfully");
            }
            else
            {
                var exception = new Exception($"User `{user.Email}` cannot be created");
                Logger.LogError("Registration failed", exception);
                throw exception;
            }

            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(createdUser, Roles.User);
        }
    }
}