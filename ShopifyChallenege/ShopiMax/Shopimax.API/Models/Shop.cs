using System;
using System.Collections;
using System.Collections.Generic;

//Shop Model storing shop details
namespace Shopimax.API.Models
{
    public class Shop
    {

        //Primary Key Attribute
        public int ShopID { get; set; }
        public string ShopName { get; set; }
        public string ShopStreet { get; set; }
        public string ShopCity { get; set; }
        public string ShopState { get; set; }
        public string ShopCountry { get; set; }
        public string ShopZip {get; set;}
        public ICollection<Product> Products { get; set; }
        public ICollection<Order> Orders { get; set; }
    
    }
}