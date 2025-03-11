using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public class SupplierData : ISupplierData
    {
        private readonly ISqlDataAccess _db;
        private const int LowStockThreshold = 10; // Define the threshold for low stock

        public SupplierData(ISqlDataAccess db)
        {
            _db = db;
        }

        // Get a list of all suppliers
        public async Task<List<SupplierModel>> GetSuppliers()
        {
            var sql = "SELECT supplier_id, name, contact_person, phone_number, address FROM Supplier ORDER BY name";
            var result = await _db.LoadData<SupplierModel, dynamic>(sql, new { });
            return result.ToList();
        }

        // Get low stock products (stock <= threshold)
        public async Task<List<ProductModel>> GetLowStockProducts()
        {
            string sql = "SELECT * FROM Product WHERE Quantity_available <= @Threshold";
            var result = await _db.LoadData<ProductModel, dynamic>(sql, new { Threshold = LowStockThreshold });
            return result.ToList();
        }

        // Notify suppliers about low stock and log the notification in the database
        public async Task NotifySuppliersAboutLowStock()
        {
            // Fetch all products with low stock levels
            var lowStockProducts = await GetLowStockProducts();

            foreach (var product in lowStockProducts)
            {
                var supplierId = product.Supplier_Id;
                var supplier = await GetSupplierById(supplierId);

                if (supplier != null)
                {
                    // Create a notification message
                    string notificationMessage = $"Stock level is low for product '{product.Name}' (Product ID: {product.Product_id}). Please restock.";
                    await SendNotificationToSupplier(supplier, notificationMessage);

                    // Log the notification in the SupplierNotifications table
                    await LogSupplierNotification(supplierId, product.Product_id, notificationMessage);
                }
            }
        }

        // Fetch supplier details by SupplierId
        private async Task<SupplierModel> GetSupplierById(int supplierId)
        {
            string sql = "SELECT * FROM Supplier WHERE supplier_id = @SupplierId";
            var result = await _db.LoadData<SupplierModel, dynamic>(sql, new { SupplierId = supplierId });
            return result.FirstOrDefault();
        }

        // Simulate sending a notification to a supplier (e.g., email, SMS)
        private async Task SendNotificationToSupplier(SupplierModel supplier, string message)
        {
            // Here, implement actual notification logic such as sending an email or SMS
            Console.WriteLine($"Sending notification to {supplier.Name}: {message}");
            await Task.CompletedTask; // Simulating async operation
        }

        // Log the notification to the SupplierNotifications table
        private async Task LogSupplierNotification(int supplierId, int productId, string message)
        {
            string sql = @"
                INSERT INTO SupplierNotifications (supplier_id, product_id, notification_message, notification_date)
                VALUES (@SupplierId, @ProductId, @Message, GETDATE())";

            await _db.SaveData(sql, new { SupplierId = supplierId, ProductId = productId, Message = message });
        }
    }
}
