using DataAccessLibrary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            string sql = "select * from Product";

            return _db.LoadData<ProductModel, dynamic>(sql, new { });
        }

        public async Task InsertProduct(ProductModel product)
        {
            if (string.IsNullOrWhiteSpace(product.Name))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }

            string sql = @"INSERT INTO Product (name, description, unit_price, quantity_available)  
                   VALUES (@Name, @Description, @Unit_price, @Quantity_available);";

            await _db.SaveData(sql, product);
        }

        public async Task DeleteProduct(int productId)
        {
            string sql = "DELETE FROM Product WHERE Product_id = @Product_id;";
            await _db.SaveData(sql, new { Product_id = productId });
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

            string sql = @"UPDATE Product SET name = @Name, description = @Description, unit_price = @Unit_price, quantity_available = @Quantity_available
                           WHERE Product_id = @Product_id;";

            await _db.SaveData(sql, product);
        }




    }
}
