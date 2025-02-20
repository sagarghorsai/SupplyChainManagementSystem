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
            string sql = "select *from Product";

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

    }
}
