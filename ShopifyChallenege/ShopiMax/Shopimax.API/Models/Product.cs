//Product Model containg product details
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopimax.API.Models
{
    public class Product
    {
        //Primary Key Attribute
        public int ProductID { get; set; }
        //Foreign Key Attribute
        public string ProductName { get; set; }
        
        public decimal ProductRate { get; set; }
        public string ProductAvailableQty { get; set; }
        public bool ProductAvailable { get; set; }
        public string ProductCategory { get; set; }
        public Shop Shops { get; set; }
        public int ShopID { get; set; }
        //Foreign Key Attribute
        //public ICollection<LineItem> LineItems { get; set; }
     

    }
}