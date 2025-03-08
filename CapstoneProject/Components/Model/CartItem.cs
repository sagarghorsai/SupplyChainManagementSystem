namespace CapstoneProject.Components.Model
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int SupplierId { get; set; } // New property for Supplier ID
    }

}
