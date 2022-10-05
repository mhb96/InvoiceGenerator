using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Repository.Models.User;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository.DataServices.Interfaces
{
    /// <summary>
    /// The user data service interface.
    /// </summary>
    public interface IUserDataService
    {
        /// <summary>
        /// Gets the user by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>
        /// The user.
        /// </returns>
        public Task<User> GetAsync(string username);

        /// <summary>
        /// Gets the user by account identifier
        /// </summary>
        /// <param name="username"></param>
        /// <returns>
        /// The user.
        /// </returns>
        public Task<UserModel> GetAsync(long accountId);

        /// <summary>
        /// Gets the user details for invoice.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>
        /// The UserDetailsForInvoiceModel model.
        /// </returns>
        public Task<UserDetailsForInvoiceModel> GetDetailsForInvoice(long accountId);

        /// <summary>
        /// Signs in user asynchronously.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>True if success; else false.</returns>
        public Task<bool> SignInAsync(SignInDataModel input);

        /// <summary>
        /// Registers users asynchronously.
        /// </summary>
        /// <param name="input"></param>
        public Task RegisterAsync(RegisterDataModel input);

        /// <summary>
        /// Updates user info asynchronously.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Task UpdateAsync(UpdateDataModel input);
    }
}
