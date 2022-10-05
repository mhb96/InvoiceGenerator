using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using InvoiceGenerator.Repository.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices
{
    /// <summary>
    /// The user data service class.
    /// </summary>
    public class UserDataService : BaseDataService, IUserDataService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IFileHelper _fileHelper;
        public UserDataService(IUnitOfWork unitOfWork, ILogger<UserDataService> logger, UserManager<User> userManager, SignInManager<User> signInManager, IFileHelper fileHelper) : base(unitOfWork, logger)
        {
            _fileHelper = fileHelper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        ///<inheritdoc/>
        public async Task<User> GetAsync(string username) =>
            await UnitOfWork.Query<User>(u => u.UserName == username).FirstOrDefaultAsync();

        ///<inheritdoc/>
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
                CompanyLogoId = u.CompanyLogo != null ? u.CompanyLogo.Id : 0,
                CompanyLogo = u.CompanyLogo != null ? _fileHelper.GetImageAddress(u.CompanyLogo.ImageName, false) : null
            }).FirstOrDefaultAsync();
        }

        ///<inheritdoc/>
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

        ///<inheritdoc/>
        public async Task<bool> SignInAsync(SignInDataModel input)
        {
            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, true, false);
            if (result.Succeeded)
            {
                var user = await GetAsync(input.Username);
                await AddClaim(user, "UserId", $"{user.Id}");
                await AddClaim(user, "FullName", $"{user.FirstName} {user.LastName}");
                return true;
            }
            return false;
        }

        ///<inheritdoc/>
        public async Task RegisterAsync(RegisterDataModel input)
        {
            if (await _userManager.FindByEmailAsync(input.Email) != null)
                throw new IGException($"User with email {input.Email} already exists. If you have forgotten your password please contact the developer.");

            Logger.LogInformation($"Create user with email `{input.Email}` for application");
            var user = new User
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                NormalizedEmail = input.Email,
                BusinessEmail = input.Email,
                UserName = input.UserName,
                NormalizedUserName = input.UserName,
                Password = input.Password,
                CompanyName = input.CompanyName,
                ContactNo = input.ContactNo,
                Address = input.Address,
                VAT = input.VAT,
                CurrencyId = input.CurrencyId,
                CompanyLogo = input.CompanyLogo,
                AccessFailedCount = 0,
                EmailConfirmed = true,
                LockoutEnabled = false,
            };

            try
            {
                var result = await _userManager.CreateAsync(user, input.Password);
                if (result.Succeeded)
                    Logger.LogInformation($"Created user `{user.Email}` successfully");
                else
                    throw new IGException($"User `{user.Email}` could not be created");
            }
            catch (IGException ex)
            {
                Logger.LogError("Registration failed", ex);
                throw new IGException($"User `{user.Email}` could not be created");
            }

            var createdUser = await _userManager.FindByEmailAsync(user.Email);
            await _userManager.AddToRoleAsync(createdUser, Roles.User);
            await UnitOfWork.SaveAsync();
        }

        ///<inheritdoc/>
        public async Task UpdateAsync(UpdateDataModel input)
        {
            User user = await UnitOfWork.FirstOrDefaultAsync<User>(u => u.Id == input.Id);
            if (user == null)
            {
                Logger.LogError($"Could not find user: {input.Id}");
                throw new IGException($"Could not find user: {input.Id}");
            }

            user.FirstName = input.FirstName;
            user.LastName = input.LastName;
            user.Email = input.Email;
            user.NormalizedEmail = input.Email.ToUpper();
            user.CompanyName = input.CompanyName;
            user.ContactNo = input.ContactNo;
            user.Address = input.Address;
            user.VAT = input.Vat;
            user.CurrencyId = input.CurrencyId;
            user.CompanyLogo = input.CompanyLogo;

            await _userManager.UpdateAsync(user);
            await AddClaim(user, "FullName", $"{user.FirstName} {user.LastName}");
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
