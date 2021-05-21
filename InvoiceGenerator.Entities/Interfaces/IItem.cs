namespace InvoiceGenerator.Entities.Interfaces
{
    /// <summary>
    /// Defins the item entity
    /// </summary>
    public interface IItem : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the product/service name.
        /// </summary>
        /// <value>
        /// The product/service name.
        /// </value>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the invoice no.
        /// </summary>
        /// <value>
        /// The invoice no.
        /// </value>
        long InvoiceNo { get; set; }

        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        /// <value>
        /// The invoice.
        /// </value>
        IInvoice Invoice { get; set; }
    }
}