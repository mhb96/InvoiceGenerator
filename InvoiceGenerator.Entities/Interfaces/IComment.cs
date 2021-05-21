namespace InvoiceGenerator.Entities.Interfaces
{
    /// <summary>
    /// Defins the comment entity.
    /// </summary>
    public interface IComment : IBaseEntity
    {
        /// <summary>
        /// Gets or sets the comment value.
        /// </summary>
        /// <value>
        /// The comment value.
        /// </value>
        string Value { get; set; }

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