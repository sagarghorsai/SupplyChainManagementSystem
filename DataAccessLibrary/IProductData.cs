using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IProductData
    {
        Task<List<ProductModel>> GetProduct();
        Task InsertProduct(ProductModel product);
    }
}