using System;
using System.Collections.Generic;

namespace InvoiceGenerator.Entities.Interfaces
{
    /// <summary>
    /// Defines the invoice interface.
    /// </summary>
    public interface IInvoice : IBaseEntity
    { /// <summary>
      /// Gets or sets the client name.
      /// </summary>
      /// <value>
      /// The client name.
      /// </value>
        string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the company name.
        /// </summary>
        /// <value>
        /// The company name.
        /// </value>
        string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the Address.
        /// </summary>
        /// <value>
        /// The Address.
        /// </value>
        string Address { get; set; }

        /// <summary>
        /// Gets or sets the due date.
        /// </summary>
        /// <value>
        /// The due date.
        /// </value>
        DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the list of comments.
        /// </summary>
        /// <value>
        /// The list of comments.
        /// </value> 
        List<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets the list of items.
        /// </summary>
        /// <value>
        /// The list of items.
        /// </value> 
        List<Item> Items { get; set; }
    }
}
