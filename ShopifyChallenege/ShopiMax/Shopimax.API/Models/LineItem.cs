//LineItem model consists of a row of ordered products and corresponding rate
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopimax.API.Models
{
    public class LineItem
    {
        //Primary Key Attribute
        public int LineItemID { get; set; }

        [Column(TypeName="Money")]
        public decimal LineItemTotalPrice { get; set; }   
        public int LineItemQuantity { get; set; }    
        //Foreign Key Attribute
        public Product Products { get; set; }
        public int? ProductID { get; set; }
        public Order Orders { get; set; }
        public int OrderID { get; set; }

    }
}