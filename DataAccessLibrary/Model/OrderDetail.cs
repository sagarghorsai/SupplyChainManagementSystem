using System.Collections.Generic;

namespace DataAccessLibrary.Model
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public decimal Total_Amount { get; set; }
        public List<OrderItemModel> Items { get; set; } = new List<OrderItemModel>();
    }
}
