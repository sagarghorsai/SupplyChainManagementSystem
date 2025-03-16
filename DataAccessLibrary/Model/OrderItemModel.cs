namespace DataAccessLibrary.Model
{
    public class OrderItemModel
    {
        public string Name { get; set; }
        public int Quantity_ordered { get; set; }  // This should map to quantity_ordered
        public decimal Price { get; set; } // This should map to price
    }

}
