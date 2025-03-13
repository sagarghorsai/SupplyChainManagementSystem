using System;

namespace DataAccessLibrary.Model
{
    public class ShipmentModel
    {
        public int Order_Id { get; set; }
        public DateTime Shipment_Date { get; set; }
        public DateTime Estimated_Arrival_Date { get; set; }
        public DateTime? Actual_Arrival_Date { get; set; }
        public string Shipment_Confirmation { get; set; } // Now supports "Pending", "Delivered", "Shipped"
    }
}
