using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductModel> Items { get; set; }

    }
}
