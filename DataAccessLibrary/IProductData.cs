using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IProductData
    {
        Task DeleteProduct(int productId);
        Task<List<ProductModel>> GetProduct();
        Task InsertProduct(ProductModel product);
        Task<List<ProductModel>> SearchProduct(string searchTerm);
        Task UpdateProduct(ProductModel product);
    }
}