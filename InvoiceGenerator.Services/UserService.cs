using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.DataTypes;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.Image;
using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository;
using InvoiceGenerator.Services.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IFileHelper _fileHelper;

        public UserService(IUnitOfWork unitOfWork, ILogger<BaseService> logger, UserManager<User> userManager, SignInManager<User> signInManager, IFileHelper fileHelper) : base(unitOfWork, logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _fileHelper = fileHelper;
        }

        public async Task<User> GetAsync(string username) =>
            await UnitOfWork.Query<User>(u => u.UserName == username).FirstOrDefaultAsync();

        public async Task<UserModel> GetAsync(long accountId)
        {
            return await UnitOfWork.Query<User>(u => u.Id == accountId).Select(u => new UserModel
            {
                Id = u.Id,
                UserName = u.UserName,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Address = u.Address,
                CompanyName = u.CompanyName,
                ContactNo = u.ContactNo,
                Email = u.Email,
                VAT = u.VAT,
                Password = u.Password,
                CurrencyId = u.CurrencyId,
                CompanyLogo = u.CompanyLogo != null ? _fileHelper.GetImageAddress(u.CompanyLogo.ImageName, false) : null
            }).FirstOrDefaultAsync();
        }

        public async Task<UserDetailsForInvoiceModel> GetDetailsForInvoice(long accountId)
        {
            return await UnitOfWork.Query<User>(u => u.Id == accountId).Select(u => new UserDetailsForInvoiceModel
            {
                Address = u.Address,
                CompanyName = u.CompanyName,
                ContactNo = u.ContactNo,
                Email = u.Email,
                Logo = u.CompanyLogo != null ? _fileHelper.GetImageAddress(u.CompanyLogo.ImageName, false) : null,
                Vat = u.VAT,
                CurrencyId = u.CurrencyId
            }).FirstOrDefaultAsync();
        }

        public async Task<bool> SignInAsync(SignInModel input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, false);
            if (result.Succeeded)
            {
                var user = await GetAsync(input.Username);
                await AddClaim(user, "UserId", $"{user.Id}");
                await AddClaim(user, "FullName", $"{user.FirstName} {user.FirstName}");
                return true;
            }
            return false;
        }

        public async Task RegisterAsync(RegisterModel input)
        {
            if (await _userManager.FindByEmailAsync(input.Email) != null)
                throw new IGException("User with email already exists. If you have forgotten your password please contact the developer.");

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
                VAT = input.Vat,
                CurrencyId = input.CurrencyId,
                LockoutEnabled = false,
                Password = input.Password
            };

            if (input.CompanyLogo != null)
            {
                ImageModel logo = await _fileHelper.UploadAsync(input.CompanyLogo, FileType.image);

                var image = new Image
                {
                    CreatedAt = DateTime.Now,
                    ImageFile = logo.ImageFile,
                    ImageName = logo.ImageName
                };
                await UnitOfWork.AddAsync<Image>(image);

                user.CompanyLogo = image;
            }

            try
            {
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                    Logger.LogInformation($"Created user `{user.Email}` successfully");
                else
                    throw new IGException($"User `{user.Email}` cannot be created");
            }
            catch (IGException ex)
            {
                Logger.LogError("Registration failed", ex);
                throw new IGException($"User `{user.Email}` cannot be created");
            }

            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(createdUser, Roles.User);
            await UnitOfWork.SaveAsync();
        }

        public async Task UpdateAsync(UpdateModel input)
        {
            User user = await UnitOfWork.FirstOrDefaultAsync<User>(u => u.Id == input.Id);
            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Email = input.Email;
            user.CompanyName = input.CompanyName;
            user.ContactNo = input.ContactNo;
            user.Address = input.Address;
            user.VAT = input.Vat;
            user.CurrencyId = input.CurrencyId;

            if (input.CompanyLogo != null)
            {
                ImageModel logo = await _fileHelper.UploadAsync(input.CompanyLogo, FileType.image);

                var image = new Image
                {
                    CreatedAt = DateTime.Now,
                    ImageFile = logo.ImageFile,
                    ImageName = logo.ImageName
                };
                await UnitOfWork.AddAsync<Image>(image);

                user.CompanyLogo = image;
            }

            await _userManager.UpdateAsync(user);
        }

        private async Task AddClaim(User user, string claimName, string claimValue)
        {
            var claims = await _userManager.GetClaimsAsync(user);
            var newClaim = new Claim(claimName, claimValue);
            Claim oldClaim = claims.FirstOrDefault(c => c.Type == claimName);
            if (oldClaim != null)
                await _userManager.ReplaceClaimAsync(user, oldClaim, newClaim);
            else await _userManager.AddClaimAsync(user, newClaim);

            await _signInManager.RefreshSignInAsync(user);
        }
    }
}