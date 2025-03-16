using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class ProductData : IProductData
    {
        private readonly ISqlDataAccess _db;
        public ProductData(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<List<ProductModel>> GetProduct()
        {
            string sql = "SELECT * FROM Product";
            return _db.LoadData<ProductModel, dynamic>(sql, new { });
        }

        public async Task InsertProduct(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }

            string sql = @"INSERT INTO Product (name, description, unit_price, quantity_available, supplier_id)  
                           VALUES (@Name, @Description, @UnitPrice, @QuantityAvailable, @SupplierId);";

            var parameters = new
            {
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.Unit_price,
                QuantityAvailable = product.Quantity_available,
                SupplierId = product.Supplier_Id // Pass SupplierId
            };

            await _db.SaveData(sql, parameters);
        }

        public async Task DeleteProduct(int productId)
        {
            string sql = "DELETE FROM Product WHERE Product_id = @ProductId;";
            await _db.SaveData(sql, new { ProductId = productId });
        }

        public Task<List<ProductModel>> SearchProduct(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException("Search term cannot be null or empty.");
            }

            string sql = "SELECT * FROM Product WHERE name LIKE @SearchTerm;";
            return _db.LoadData<ProductModel, dynamic>(sql, new { SearchTerm = "%" + searchTerm + "%" });
        }

        public async Task UpdateProduct(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }

            string sql = @"UPDATE Product 
                           SET name = @Name, description = @Description, unit_price = @UnitPrice, 
                               quantity_available = @QuantityAvailable, supplier_id = @SupplierId
                           WHERE Product_id = @ProductId;";

            var parameters = new
            {
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.Unit_price,
                QuantityAvailable = product.Quantity_available,
                SupplierId = product.Supplier_Id, // Pass SupplierId
                ProductId = product.Product_id
            };

            await _db.SaveData(sql, parameters);
        }
    }
}
