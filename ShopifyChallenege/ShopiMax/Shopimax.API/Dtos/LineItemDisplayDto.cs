namespace Shopimax.API.Dtos
{
    public class LineItemDisplayDto
    {
        public int LineItemID { get; set; }

        public string LineItemTotalPrice { get; set; }   
        public int LineItemQuantity { get; set; }    
        //Foreign Key Attribute
     
        public int? ProductID { get; set; }

        public int OrderID { get; set; }
        
    }
}