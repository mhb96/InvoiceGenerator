using InvoiceGenerator.Entities;
using Microsoft.AspNetCore.Http;

namespace InvoiceGenerator.Repository.Models.User
{
    public class RegisterDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string BusinessEmail { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public decimal VAT { get; set; }
        public long CurrencyId { get; set; }
        public Image CompanyLogo { get; set; }
    }
}
