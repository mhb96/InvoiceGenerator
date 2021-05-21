namespace InvoiceGenerator.Entities.Interfaces
{
    /// <summary>
    ///  Defines the user entity.
    /// </summary>
    public interface IUser: IBaseEntity
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the Company Name.
        /// </summary>
        /// <value>
        /// The Company Name.
        /// </value> 
        string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the contact no.
        /// </summary>
        /// <value>
        /// The contact no.
        /// </value> 
        string ContactNo { get; set; } // we already have phone number in the identiy

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        /// <value>
        /// The Address.
        /// </value> 
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the VAT.
        /// </summary>
        /// <value>
        /// The VAT.
        /// </value> 
        decimal VAT { get; set; }

        /// <summary>
        /// Gets or sets the Company Logo.
        /// </summary>
        /// <value>
        /// The Company Logo.
        /// </value> 
        string CompanyLogo { get; set; }
    }
}
