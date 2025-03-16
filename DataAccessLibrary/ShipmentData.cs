using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLibrary.Model;
using Microsoft.Data.SqlClient;

namespace DataAccessLibrary 
{


    public class ShipmentData : IShipmentData
    {
        private readonly ISqlDataAccess _db;

        public ShipmentData(ISqlDataAccess db)
        {
            _db = db;
        }

        // Retrieve all shipments
        public async Task<List<ShipmentModel>> GetAllShipments()
        {
            string sql = "SELECT * FROM Shipment";
            return await _db.LoadData<ShipmentModel, dynamic>(sql, new { });
        }

        // Insert new shipment
        public async Task InsertShipment(ShipmentModel shipment)
        {
            if (string.IsNullOrWhiteSpace(shipment.Shipment_Confirmation))
            {
                shipment.Shipment_Confirmation = "Pending"; // Default status
            }

            string sql = @"
                INSERT INTO Shipment 
                (Order_Id, Shipment_Date, Estimated_Arrival_Date, Actual_Arrival_Date, Shipment_Confirmation)
                VALUES 
                (@OrderId, @ShipmentDate, @EstimatedArrivalDate, @ActualArrivalDate, @ShipmentConfirmation);";

            await _db.SaveData(sql, new
            {
                shipment.Order_Id,
                shipment.Shipment_Date,
                shipment.Estimated_Arrival_Date,
                shipment.Actual_Arrival_Date,
                shipment.Shipment_Confirmation
            });
        }

        // Update shipment status
        public async Task UpdateShipmentStatus(int orderId, string status)
        {
            var validStatuses = new List<string> { "Pending", "Shipped", "Delivered" };
            if (!validStatuses.Contains(status))
            {
                throw new ArgumentException("Invalid shipment status. Must be 'Pending', 'Shipped', or 'Delivered'.");
            }

            string sql = @"
                UPDATE Shipment 
                SET Shipment_Confirmation = @ShipmentConfirmation 
                WHERE Order_Id = @OrderId;";

            await _db.SaveData(sql, new { OrderId = orderId, ShipmentConfirmation = status });
        }

        // Get shipment by OrderId
        public async Task<ShipmentModel?> GetShipmentByOrderId(int orderId)
        {
            string sql = @"SELECT * FROM Shipment WHERE Order_Id = @OrderId;";
            var result = await _db.LoadData<ShipmentModel, dynamic>(sql, new { OrderId = orderId });
            return result.FirstOrDefault();
        }

        public async Task<OrderDetail> GetOrderDetails(int orderId)
        {
            string sql = @"
    SELECT 
        o.order_id,
        u.user_name AS CustomerName,
        u.email AS CustomerEmail,
        o.total_amount
    FROM Orders o
    JOIN Users u ON o.user_id = u.user_id  
    WHERE o.order_id = @OrderId";

            var orderDetails = await _db.LoadData<OrderDetail, dynamic>(sql, new { OrderId = orderId });

            if (!orderDetails.Any())
            {
                throw new Exception("Order not found.");
            }

            var details = orderDetails.First();

            // Fetch order items
            string itemsSql = @"
SELECT 
    p.name,
    od.quantity_ordered,
    od.price
FROM OrderDetails od
JOIN Product p ON od.product_id = p.product_id
WHERE od.order_id = @OrderId";

            var items = await _db.LoadData<OrderItemModel, dynamic>(itemsSql, new { OrderId = orderId });

            foreach (var item in items)
            {
                // Log the raw data for debugging
                Console.WriteLine($"Item: {item.Name}, Quantity Ordered: {item.Quantity_ordered}, Price: {item.Price}");
            }

            details.Items = items;


            return details;

        }

    }
}
