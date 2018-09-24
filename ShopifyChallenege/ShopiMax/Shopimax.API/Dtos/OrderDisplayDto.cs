using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopimax.API.Dtos
{
    public class OrderDisplayDto
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public int ShopID { get; set; }
        public bool OrderCancelled { get; set; }
        public DateTime OrderDate { get; set;}
        
       
        public string OrderTotal { get; set; }
        
    }
}