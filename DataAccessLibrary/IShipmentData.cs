using DataAccessLibrary.Model;

namespace DataAccessLibrary
{
    public interface IShipmentData
    {
        Task<List<ShipmentModel>> GetAllShipments();
        Task<OrderDetail> GetOrderDetails(int orderId);
        Task<ShipmentModel?> GetShipmentByOrderId(int orderId);
        Task InsertShipment(ShipmentModel shipment);
        Task UpdateShipmentStatus(int orderId, string status);
    }
}