﻿using InvoiceGenerator.Common.Models.User;
using InvoiceGenerator.Entities;
using InvoiceGenerator.Services.Models.User;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public interface IUserService : IBaseService
    {
        public Task<UserModel> GetAsync(long id);
        public Task<User> GetAsync(string username);
        public Task<bool> SignInAsync(SignInModel input);
        public Task RegisterAsync(RegisterModel input);
        public Task<UserDetailsForInvoiceModel> GetDetailsForInvoice(long accountId);
        public Task UpdateAsync(UpdateModel input);
    }
}