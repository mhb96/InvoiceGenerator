﻿namespace InvoiceGenerator.Entities
{
    public class User : BaseEntity
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
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the contact no.
        /// </summary>
        /// <value>
        /// The contact no.
        /// </value> 
        public string ContactNo { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        /// <value>
        /// The Address.
        /// </value> 
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the VAT.
        /// </summary>
        /// <value>
        /// The VAT.
        /// </value> 
        public decimal VAT { get; set; }

        /// <summary>
        /// Gets or sets the Company Logo.
        /// </summary>
        /// <value>
        /// The Company Logo.
        /// </value> 
        public string CompanyLogo { get; set; }

        /// <summary>
        /// Gets or sets the is admin boolean.
        /// </summary>
        /// <value>
        /// The is admin boolean.
        /// </value> 
        public bool IsAdmin { get; set; }
    }
}
