using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Model
{
    public class ProductModel
    {
        
        public int Product_id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal unit_price { get; set; }

        public int quantity_available { get; set; } = 0;
    }
}
