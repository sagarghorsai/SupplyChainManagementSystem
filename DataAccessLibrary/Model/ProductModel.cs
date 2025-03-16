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
        public decimal Unit_price { get; set; }
        public int Quantity_available { get; set; } = 0;
        public int Supplier_Id { get; set; }

        // Parameterless Constructor
        public ProductModel() { }
        public ProductModel(int product_id,string name, string description, decimal unitPrice, int quantityAvailable = 0)
        {
            Product_id = product_id;
            Name = name;
            Description = description;
            Unit_price = unitPrice;
            Quantity_available = quantityAvailable;
        }
        public ProductModel(int product_id, string name, string description, decimal unitPrice, int supplierId, int quantityAvailable = 0)
        {
            Product_id = product_id;
            Name = name;
            Description = description;
            Unit_price = unitPrice;
            Supplier_Id = supplierId;
            Quantity_available = quantityAvailable;
        }
        public ProductModel( string name, string description, decimal unitPrice, int supplierId, int quantityAvailable = 0)
        {
            Name = name;
            Description = description;
            Unit_price = unitPrice;
            Supplier_Id = supplierId;
            Quantity_available = quantityAvailable;
        }

        public ProductModel( string name, string description, decimal unitPrice, int quantityAvailable = 0)
        {
            Name = name;
            Description = description;
            Unit_price = unitPrice;
            Quantity_available = quantityAvailable;
        }
    }
}
