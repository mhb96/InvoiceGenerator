namespace InvoiceGenerator.Common.Models.User
{
    public class UserModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public decimal VAT { get; set; }
        public string Email { get; set; }
        public string CompanyLogo { get; set; }
        public string Password { get; set; }
    }
}
