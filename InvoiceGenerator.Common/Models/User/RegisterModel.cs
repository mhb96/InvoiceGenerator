using Microsoft.AspNetCore.Http;

namespace InvoiceGenerator.Common.Models.User
{
    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public decimal Vat { get; set; }
        public IFormFile CompanyLogo { get; set; }
    }
}
