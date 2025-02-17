namespace CapstoneProject.Components.Models
{
    public class ProductItem
    {
        public int ProductId { get; set; }           // Unique ID for the product
        public string Name { get; set; }             // Product name
        public string Description { get; set; }      // Product description
        public decimal UnitPrice { get; set; }       // Price per unit of the product
        public int QuantityAvailable { get; set; }   // Quantity of the product in stock
    }
}
