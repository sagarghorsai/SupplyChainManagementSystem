using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IStoreData
    {
        List<CartItem> Cart { get; }

        Task AddToCart(ProductModel product, int quantity, int supplierId);
        Task<OrderModel> Checkout(int userId, int supplierId);
        Task<List<ProductModel>> GetAvailableProducts();
        Task<List<CartItem>> GetCartItems();
        decimal GetTotal();
        Task<UsersModel> GetUserByUsername(string username);
        Task RemoveFromCart(int productId);
        Task SaveOrder(int customerId, int supplierId);
    }
}