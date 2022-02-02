using InvoiceGenerator.Entities.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements the user entity.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Identity.IdentityUser{System.Int64}" />
    /// <seealso cref="InvoiceGenerator.Entities.Interfaces.IUser" />
    public class User : IdentityUser<long>, IBaseEntity
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
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
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
        /// Gets or sets the business email.
        /// </summary>
        /// <value>
        /// The business email.
        /// </value> 
        public string BusinessEmail { get; set; }

        /// <summary>
        /// Gets or sets the Company Logo.
        /// </summary>
        /// <value>
        /// The Company Logo.
        /// </value> 
        public Image CompanyLogo { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is deleted; otherwise, <c>false</c>.
        /// </value>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets the created at.
        /// </summary>
        /// <value>
        /// The created at.
        /// </value>
        public DateTime CreatedAt { get; set; }
    }
}
