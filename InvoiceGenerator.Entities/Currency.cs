namespace InvoiceGenerator.Entities
{
    /// <summary>
    /// Implements the currency entity.
    /// </summary>
    /// <seealso cref="InvoiceGenerator.Entities.BaseEntity" />
    public class Currency : BaseEntity
    {
        /// <summary>
        /// Gets or sets the currency name.
        /// </summary>
        /// <value>
        /// The currency name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the currency code.
        /// </summary>
        /// <value>
        /// The currency code.
        /// </value>
        public string Code { get; set; }
    }
}
