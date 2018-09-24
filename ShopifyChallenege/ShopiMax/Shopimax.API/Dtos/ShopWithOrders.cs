using System.Collections.Generic;
using Shopimax.API.Models;

namespace Shopimax.API.Dtos
{
    public class ShopWithOrders
    {
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopStreet { get; set; }
        public string ShopCity { get; set; }
        public string ShopState { get; set; }
        public string ShopCountry { get; set; }
        public string ShopZip {get; set;}
        
        public ICollection<Order> Orders { get; set; }
    }
}