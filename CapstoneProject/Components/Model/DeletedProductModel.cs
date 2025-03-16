namespace CapstoneProject.Components.Model
{
    public class DeletedProductModel
    {
        public int Product_id { get; set; }

        public int Supplier_id { get; set; } // Optional, if needed

        public DeletedProductModel() { }

        public DeletedProductModel(int productId, int supplierId)
        {
            Product_id = productId;
            Supplier_id = supplierId; // Initialize with supplier_id if needed
        }
    }
}
