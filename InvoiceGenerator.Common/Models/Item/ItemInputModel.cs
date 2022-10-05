namespace InvoiceGenerator.Common.Models.Item
{
    /// <summary>
    /// The item input model.
    /// </summary>
    public class ItemInputModel
    {
        /// <summary>
        /// The name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The quantity.
        /// </summary>
        public decimal Quantity { get; set; }

        /// <summary>
        /// The unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
