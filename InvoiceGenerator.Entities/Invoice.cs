using System;
using System.Collections.Generic;

namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements the invoice entity.
    /// </summary>
    /// <seealso cref="InvoiceGenerator.Entities.BaseEntity" />
    /// <seealso cref="InvoiceGenerator.Entities.Interfaces.IInvoice" />
    public class Invoice : BaseEntity
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>
        /// The client name.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the client company name.
        /// </summary>
        /// <value>
        /// The client company name.
        /// </value>
        public string ClientCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string ClientAddress { get; set; }

        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        /// <value>
        /// The phone number.
        /// </value>
        public string ClientPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        /// <value>
        /// The email address.
        /// </value>
        public string ClientEmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        /// <value>
        /// The comment.
        /// </value> 
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        /// <value>
        /// The list of items.
        /// </value> 
        public virtual List<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the fee paid.
        /// </summary>
        /// <value>
        /// The fee paid.
        /// </value> 
        public decimal FeePaid { get; set; }

        /// <summary>
        /// Gets or sets the total fee.
        /// </summary>
        /// <value>
        /// The total fee.
        /// </value> 
        public decimal TotalFee { get; set; }

        /// <summary>
        /// Gets or sets the vat.
        /// </summary>
        /// <value>
        /// The vat.
        /// </value> 
        public decimal Vat { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value>
        public long CurrencyId { get; set; }

        /// <summary>
        /// Gets or sets the currency.
        /// </summary>
        /// <value>
        /// The currency.
        /// </value>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Gets or sets the user Company Name.
        /// </summary>
        /// <value>
        /// The user Company Name.
        /// </value> 
        public string UserCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the user contact no.
        /// </summary>
        /// <value>
        /// The user contact no.
        /// </value> 
        public string UserContactNo { get; set; }

        /// <summary>
        /// Gets or sets the user Address.
        /// </summary>
        /// <value>
        /// The user Address.
        /// </value> 
        public string UserAddress { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value> 
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the User Company Logo.
        /// </summary>
        /// <value>
        /// The User Company Logo.
        /// </value> 
        public long UserCompanyLogoId { get; set; }
    }
}