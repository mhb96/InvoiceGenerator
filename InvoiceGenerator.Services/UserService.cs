using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Extensions;
using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using InvoiceGenerator.Repository.Models.User;
using InvoiceGenerator.Services.Models.User;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserDataService _userDataService;
        private readonly IImageService _imageService;

        public UserService(ILogger<BaseService> logger, IUserDataService userDataService, IImageService imageService) : base(logger)
        {
            _userDataService = userDataService;
            _imageService = imageService;
        }

        public async Task<User> GetAsync(string username) => await _userDataService.GetAsync(username);

        public async Task<UserModel> GetAsync(long accountId)
        {

            UserModel user = await _userDataService.GetAsync(accountId);
            if (user == null)
            {
                Logger.LogError("User not found");
                throw new IGException("User not found");
            }

            return user;
        }

        public async Task<UserDetailsForInvoiceModel> GetDetailsForInvoice(long accountId)
        {
            var user = await _userDataService.GetDetailsForInvoice(accountId);
            if (user == null)
            {
                Logger.LogError("User not found");
                throw new IGException("User not found");
            }

            return user;
        }

        public async Task<bool> SignInAsync(SignInModel input) => await _userDataService.SignInAsync(new SignInDataModel
        {
            Username = input.Username,
            Password = input.Password
        });

        public async Task RegisterAsync(RegisterModel input)
        {
            input.FirstName.ValidateString("First name", RegexStrings.Name, InputLengthLimits.Name);
            input.LastName.ValidateString("Last name", RegexStrings.Name, InputLengthLimits.Name);
            Validate.ValidateEmail(input.Email);
            input.Address.ValidateString("Address", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            input.ContactNo.ValidateString("Contact no.", RegexStrings.ContactNo, InputLengthLimits.ContactNo);
            input.CompanyName.ValidateString("Company name", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            input.UserName.ValidateString("Username", RegexStrings.Username, InputLengthLimits.Name);
            input.Password.ValidateString("Password", RegexStrings.Password, InputLengthLimits.Password);

            if (await GetAsync(input.UserName) != null)
                throw new IGException($"User with username {input.UserName} already exists. If you have forgotten your password please contact the developer.");

            var user = new RegisterDataModel
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                NormalizedEmail = input.Email.ToUpper(),
                BusinessEmail = input.Email,
                UserName = input.UserName,
                NormalizedUserName = input.UserName.ToUpper(),
                Password = input.Password,
                CompanyName = input.CompanyName,
                ContactNo = input.ContactNo,
                Address = input.Address,
                VAT = input.Vat,
                CurrencyId = input.CurrencyId
            };

            if (input.CompanyLogo != null)
                user.CompanyLogo = await _imageService.AddAsync(input.CompanyLogo);

            await _userDataService.RegisterAsync(user);
        }

        public async Task UpdateAsync(UpdateModel input)
        {
            input.FirstName.ValidateString("First name", RegexStrings.Name, InputLengthLimits.Name);
            input.LastName.ValidateString("Last name", RegexStrings.Name, InputLengthLimits.Name);
            Validate.ValidateEmail(input.Email);
            input.Address.ValidateString("Address", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            input.ContactNo.ValidateString("Contact no.", RegexStrings.ContactNo, InputLengthLimits.ContactNo);
            input.CompanyName.ValidateString("Company name", RegexStrings.ObjectName, InputLengthLimits.ObjectName);

            Image companyLogo = null;
            if (input.CompanyLogo != null)
                companyLogo = await _imageService.AddAsync(input.CompanyLogo);

            await _userDataService.UpdateAsync(new UpdateDataModel
            {
                Id = input.Id,
                FirstName = input.FirstName,
                LastName = input.LastName,
                Email = input.Email,
                CompanyName = input.CompanyName,
                ContactNo = input.ContactNo,
                Address = input.Address,
                Vat = input.Vat,
                CurrencyId = input.CurrencyId,
                CompanyLogo = companyLogo
            });
        }
    }
}