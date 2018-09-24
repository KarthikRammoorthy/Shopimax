namespace Shopimax.API.Dtos
{
    public class ProductDisplayDto
    {
        public int ProductID { get; set; }
        //Foreign Key Attribute
        public string ProductName { get; set; }
        public string ProductRate { get; set; }
        public string ProductAvailableQty { get; set; }
        public bool ProductAvailable { get; set; }
        public string ProductCategory { get; set; }
        public int ShopID { get; set; }
        
    }
}