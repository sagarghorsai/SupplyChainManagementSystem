using Microsoft.Data.SqlClient;

namespace CapstoneProject.Components.Models
{
    public class InventoryManagement
    {

        private readonly DatabaseHelper _dbHelper;



        public List<ProductItem> GetInventory()
        {
            List<ProductItem> inventory = new List<ProductItem>();

            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                string query = "SELECT product_id, name, description, unit_price, quantity_available FROM Product";
                using (var cmd = new SqlCommand(query, connection))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            inventory.Add(new ProductItem
                            {
                                ProductId = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                UnitPrice = reader.GetDecimal(3),
                                QuantityAvailable = reader.GetInt32(4)
                            });
                        }
                    }
                }
            }

            return inventory;
        }

        public bool UpdateProductDetails(int productId, string name, string description, decimal unitPrice, int quantityAvailable)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                connection.Open();

                string updateQuery = "UPDATE Product SET name = @Name, description = @Description, unit_price = @UnitPrice, quantity_available = @QuantityAvailable WHERE product_id = @ProductId";
                using (var cmd = new SqlCommand(updateQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@UnitPrice", unitPrice);
                    cmd.Parameters.AddWithValue("@QuantityAvailable", quantityAvailable);
                    cmd.Parameters.AddWithValue("@ProductId", productId);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0; // If any row is updated, return true
                }
            }
        }
    }
}
