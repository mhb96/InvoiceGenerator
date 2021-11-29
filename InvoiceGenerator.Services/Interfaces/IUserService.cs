using InvoiceGenerator.Common.Models;
using InvoiceGenerator.Common.Models.User;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IUserService : IBaseService
    {
        public Task<UserModel> GetAsync(string username);
        public Task<UserModel> GetAsync(long id);
        public Task RegisterAsync(RegisterModel input);
    }
}