using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface ISupplierData
    {
        Task<List<ProductModel>> GetLowStockProducts();
        Task<List<SupplierModel>> GetSuppliers();
        Task NotifySuppliersAboutLowStock();
    }
}