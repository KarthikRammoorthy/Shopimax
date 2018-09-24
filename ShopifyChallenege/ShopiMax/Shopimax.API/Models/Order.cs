using System;
using System.Collections.Generic;
//Order consists of one or more line items with order details
namespace Shopimax.API.Models
{
    public class Order
    {
        //Primary Key Attribute
        public int OrderID { get; set; }
    
        public bool OrderCancelled { get; set; }

        public DateTime OrderDate { get; set;}
        //public LineItem LineItems { get; set; }
        //public int? LineItemID { get; set; }
         public ICollection<LineItem> LineItems { get; set; }
        public User Users { get; set; }
        public int UserID { get; set; }

        public Shop Shops { get; set; }
        public int? ShopID { get; set; }
        
        //Foreign Key Attribute
    }
}