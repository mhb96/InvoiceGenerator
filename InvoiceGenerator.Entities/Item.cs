using InvoiceGenerator.Entities.Interfaces;

namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements item entity.
    /// </summary>
    /// <seealso cref="InvoiceGenerator.Entities.BaseEntity" />
    /// <seealso cref="InvoiceGenerator.Entities.Interfaces.IItem" />
    public class Item : BaseEntity
    {
        /// <summary>
        /// Gets or sets the product/service name.
        /// </summary>
        /// <value>
        /// The product/service name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the quantity.
        /// </summary>
        /// <value>
        /// The quantity.
        /// </value>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Gets or sets the unit price.
        /// </summary>
        /// <value>
        /// The unit price.
        /// </value>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Gets or sets the invoice no.
        /// </summary>
        /// <value>
        /// The invoice no.
        /// </value>
        public long InvoiceNo { get; set; }

        /// <summary>
        /// Gets or sets the invoice.
        /// </summary>
        /// <value>
        /// The invoice.
        /// </value>
        public virtual Invoice Invoice { get; set; }
    }
}
