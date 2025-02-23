using System.ComponentModel.DataAnnotations;

namespace CapstoneProject.Components.Model
{
    public class UpdateProductModel
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Name is too long")]
        [MinLength(3, ErrorMessage = "Name is too short")]
        public string Name { get; set; } = string.Empty;  // Ensure it's never null

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters.")]
        public string Description { get; set; } = string.Empty;  // Ensure it's never null

        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than zero.")]
        public decimal Unit_price { get; set; } = 0.01M;  // Set a default value

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity_available { get; set; } = 0;
    }
}
