using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Components.Model
{
    public class UpdateProductModel
    {
        public int Product_id { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
        [MinLength(3, ErrorMessage = "Name must be at least 3 characters long.")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
        public decimal Unit_price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity available cannot be negative.")]
        public int Quantity_available { get; set; } = 0;

        [Range(1, int.MaxValue, ErrorMessage = "Supplier ID must be a positive integer.")]
        public int Supplier_id { get; set; } // Added supplier_id property

        public UpdateProductModel() { }

        public UpdateProductModel(int productId, string name, string description, decimal unitPrice, int quantityAvailable = 0, int supplierId = 1)
        {
            Product_id = productId;
            Name = name;
            Description = description;
            Unit_price = unitPrice;
            Quantity_available = quantityAvailable;
            Supplier_id = supplierId; // Initialize supplier_id
        }
    }
}
