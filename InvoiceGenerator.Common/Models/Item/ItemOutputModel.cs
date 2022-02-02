namespace InvoiceGenerator.Common.Models.Item
{
    public class ItemOutputModel
    {
        public string Name { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
