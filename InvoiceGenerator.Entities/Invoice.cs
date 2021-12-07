using InvoiceGenerator.Entities.Interfaces;
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
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        /// <value>
        /// The Address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the list of comments.
        /// </summary>
        /// <value>
        /// The list of comments.
        /// </value> 
        public List<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        /// <value>
        /// The list of items.
        /// </value> 
        public List<Item> Items { get; set; }

        /// <summary>
        /// Gets or sets the company logo id.
        /// </summary>
        /// <value>
        /// The company logo id.
        /// </value> 
        public long CompanyLogoId { get; set; }

        /// <summary>
        /// Gets or sets the company logo.
        /// </summary>
        /// <value>
        /// The company logo.
        /// </value> 
        public Image CompanyLogo { get; set; }
    }
}