using System.Collections.Generic;
using System.Threading.Tasks;
using Shopimax.API.Dtos;
using Shopimax.API.Models;

namespace Shopimax.API.Data
{
    public interface IShopiMaxRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();


         //Shops
         Task<IEnumerable<Shop>> GetShops();
         Task<Shop> GetShop(int id);
         Task<Shop> EditShop(Shop shop);
         Task<bool> ShopExists(string shopName);
         Task<IEnumerable<Shop>> GetProductsByShops();
         Task<IEnumerable<Shop>> GetOrdersByShops();

         //Products
         Task<IEnumerable<Product>> GetProducts();
         Task<Product> GetProduct(int id);
         Task<Product> EditProduct(Product product);
         Task<bool> ProductExists(string productName, int shopId);

         //Orders
         Task<IEnumerable<Order>> GetOrders();
         Task<IEnumerable<Order>> GetLineItemsbyOrders();
         Task<Order> GetOrder(int id);
         Task<Order> EditOrder(Order order);

         //LineItems
         Task<IEnumerable<LineItem>> GetLineItems();
         Task<LineItem> GetLineItem(int id);
         Task<LineItem> EditLineItem(LineItem lineItem);

         //User
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUserByID(int id);
         Task<IEnumerable<User>> GetOrdersByUsers();




       
         
         


    }
}