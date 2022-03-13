using InvoiceGenerator.Common.Models.Item;
using System.Collections.Generic;

namespace InvoiceGenerator.Common.Models.Invoice
{
    public class InvoiceModel
    {
        /// <summary>
        /// Gets or sets the invoice number.
        /// </summary>
        /// <value>
        /// The invoice number.
        /// </value>
        public string InvoiceNo { get; set; }

        /// <summary>
        /// Gets or sets the user company name.
        /// </summary>
        /// <value>
        /// The user company name.
        /// </value>
        public string UserCompanyName { get; set; }

        /// <summary>
        /// Gets or sets the user company logo id.
        /// </summary>
        /// <value>
        /// The user company logo id.
        /// </value>
        public long UserCompanyLogoId { get; set; }

        /// <summary>
        /// Gets or sets the user address.
        /// </summary>
        /// <value>
        /// The user address.
        /// </value>
        public string UserAddress { get; set; }

        /// <summary>
        /// Gets or sets the user contact no.
        /// </summary>
        /// <value>
        /// The user contact no.
        /// </value>
        public string UserContactNo { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the user logo.
        /// </summary>
        /// <value>
        /// The user logo.
        /// </value>
        public string UserCompanyLogo { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        /// <value>
        /// The client name.
        /// </value>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        /// <value>
        /// The company name.
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
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public string DueDate { get; set; }

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
        public List<ItemOutputModel> Items { get; set; }

        /// <summary>
        /// Gets or sets the sub total fee.
        /// </summary>
        /// <value>
        /// The sub total fee.
        /// </value> 
        public string SubTotalFee { get; set; }

        /// <summary>
        /// Gets or sets the total fee.
        /// </summary>
        /// <value>
        /// The total fee.
        /// </value> 
        public string TotalFee { get; set; }

        /// <summary>
        /// Gets or sets the fee paid.
        /// </summary>
        /// <value>
        /// The fee paid.
        /// </value> 
        public decimal FeePaid { get; set; }

        /// <summary>
        /// Gets or sets the total amount due.
        /// </summary>
        /// <value>
        /// The total amount due.
        /// </value> 
        public string TotalFeeDue { get; set; }

        /// <summary>
        /// Gets or sets the vat.
        /// </summary>
        /// <value>
        /// The vat.
        /// </value> 
        public decimal Vat { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value> 
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Gets or sets the currency identifier.
        /// </summary>
        /// <value>
        /// The currency identifier.
        /// </value> 
        public long CurrencyId { get; set; }
    }
}
