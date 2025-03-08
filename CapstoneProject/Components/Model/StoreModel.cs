namespace CapstoneProject.Components.Model
{
    public class StoreModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantityToBuy { get; set; }
        public int SupplierId { get; set; }

    }

}
