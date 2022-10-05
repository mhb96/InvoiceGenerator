using InvoiceGenerator.Common.Models.Image;

namespace InvoiceGenerator.Common.Models.User
{
    /// <summary>
    /// The user model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// The identifier.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// The user name.
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The company name.
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// The contact no.
        /// </summary>
        public string ContactNo { get; set; }

        /// <summary>
        /// The address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// The vat.
        /// </summary>
        public decimal VAT { get; set; }

        /// <summary>
        /// The email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The company logo.
        /// </summary>
        public string CompanyLogo { get; set; }

        /// <summary>
        /// The company logo file.
        /// </summary>
        public ImageModel CompanyLogoFile { get; set; }
        
        /// <summary>
        /// The password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The currency identifier.
        /// </summary>
        public long CurrencyId { get; set; }

        /// <summary>
        /// The company logo identifier.
        /// </summary>
        public long CompanyLogoId { get; set; }
    }
}
