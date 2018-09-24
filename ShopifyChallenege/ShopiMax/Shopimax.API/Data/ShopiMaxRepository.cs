using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Shopimax.API.Dtos;
using Shopimax.API.Models;

namespace Shopimax.API.Data
{
    public class ShopiMaxRepository : IShopiMaxRepository
    {
        private readonly DataContext _context;
        public ShopiMaxRepository(DataContext context)
        {
            _context = context;
        }
        public void Add<T>(T entity) where T : class
        {
             _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
             _context.SaveChanges();
        }
        
        //Shop CRUD operations
        public async Task<Shop> GetShop(int id)
        {
            var shop =  await _context.Shops.FirstOrDefaultAsync(u => u.ShopID == id);
            return shop;
            
        }

        public async Task<Shop> EditShop(Shop newShop)
        {
           Shop shop =  await _context.Shops.FirstOrDefaultAsync(s => s.ShopID == newShop.ShopID);
           shop.ShopName = newShop.ShopName;
           shop.ShopStreet = newShop.ShopStreet;
           shop.ShopState = newShop.ShopState;
           shop.ShopCity = newShop.ShopCity;
           shop.ShopZip = newShop.ShopZip;
           _context.Shops.Update(shop);
           await _context.SaveChangesAsync();
           return shop;
        }

        public async Task<IEnumerable<Shop>> GetShops()
        {
            var shop = await _context.Shops.ToListAsync();
            return shop;
        }

        public async Task<IEnumerable<Shop>> GetProductsByShops()
        {
            var shops = await _context.Shops.Include(p => p.Products).ToListAsync();
            return shops;
        }
        public async Task<IEnumerable<Shop>> GetOrdersByShops()
        {
            var shops = await _context.Shops.Include(p => p.Orders ).Include(p => p.Products).ToListAsync();
            return shops;
        }

        public async Task<bool> ShopExists(string shopName)
        {
            if (await _context.Shops.AnyAsync(x => x.ShopName == shopName))
                return true;

            return false;
        }

        //Order CRUD operation
        public async Task<IEnumerable<Order>> GetOrders()
        {
            var order = await _context.Orders.Include(p => p.LineItems).ToListAsync();
            return order;
        }

          public async Task<IEnumerable<Order>> GetLineItemsbyOrders()
        {
            var order = await _context.Orders.Include(p => p.LineItems).ToListAsync();
            return order;
        }

        public async Task<Order> GetOrder(int id)
        {
            var order =  await _context.Orders.Include(p => p.LineItems).FirstOrDefaultAsync(u => u.OrderID == id);
            return order;
            
        }

        public async Task<Order> EditOrder(Order newOrder)
        {
           Order order =  await _context.Orders.FirstOrDefaultAsync(s => s.OrderID == newOrder.OrderID);
           order.OrderCancelled = newOrder.OrderCancelled;
           order.OrderDate = newOrder.OrderDate;
           _context.Orders.Update(order);
           await _context.SaveChangesAsync();
           return order;
        }




        //Products CRUD Methods
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(u => u.ProductID == id);
            return product;
        }

        public async Task<Product> EditProduct(Product newProduct)
        {
           Product product =  await _context.Products.FirstOrDefaultAsync(s => s.ProductID == newProduct.ProductID);
           product.ProductName = newProduct.ProductName;
           product.ProductCategory = newProduct.ProductCategory;
           product.ProductAvailable = newProduct.ProductAvailable;
           product.ProductAvailableQty = newProduct.ProductAvailableQty;
           product.ProductRate = newProduct.ProductRate;
           _context.Products.Update(product);
           await _context.SaveChangesAsync();
           return product;
        }

         public async Task<bool> ProductExists(string productName, int shopId)
        {
            if (await _context.Products.AnyAsync(x => x.ProductName == productName && x.ShopID == shopId))
                return true;

            return false;
        }

        //LineItems CRUD Operation
        public async Task<IEnumerable<LineItem>> GetLineItems()
        {
            var lineItems = await _context.LineItems.ToListAsync();
            return lineItems;
        }

        public async Task<LineItem> GetLineItem(int id)
        {
            var lineItems = await _context.LineItems.FirstOrDefaultAsync(u => u.LineItemID == id);
            return lineItems;
        }

        public async Task<LineItem> EditLineItem(LineItem newlineItem)
        {
           LineItem lineItem =  await _context.LineItems.FirstOrDefaultAsync(s => s.LineItemID == newlineItem.LineItemID);
           lineItem.LineItemQuantity = newlineItem.LineItemQuantity;
           lineItem.LineItemTotalPrice = newlineItem.LineItemTotalPrice;
           lineItem.OrderID = newlineItem.OrderID;
           lineItem.ProductID = newlineItem.ProductID;
           _context.LineItems.Update(lineItem);
           await _context.SaveChangesAsync();
           return lineItem;
        }
         public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
        public async Task<IEnumerable<User>> GetOrdersByUsers()
        {
            var users = await _context.Users.Include(p => p.Orders).ToListAsync();
            return users;
        }

        public async Task<User> GetUserByID(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserID == id);
            return user;
        }



        public async Task<bool> SaveAll()
        {
           return await _context.SaveChangesAsync()>0;
        }

    }
}