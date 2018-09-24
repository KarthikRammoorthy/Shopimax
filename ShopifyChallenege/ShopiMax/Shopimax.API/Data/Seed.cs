using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Shopimax.API.Models;

namespace Shopimax.API.Data
{
    public class Seed
    {
         private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }
        public void SeedUserData()
        {
                var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
                var users = JsonConvert.DeserializeObject<List<User>>(userData);
                 foreach (var user in users)
                {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();
                

                _context.Users.Add(user);
               }

                _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            } 
        }

        public void SeedShopData()
        {
                var shopData = System.IO.File.ReadAllText("Data/ShopSeedData.json");
                var shops = JsonConvert.DeserializeObject<List<Shop>>(shopData);
                foreach (var shop in shops)
                {
                    _context.Shops.Add(shop);
                }

                _context.SaveChanges();
        }
              public void SeedProductData()
        {
                var productData = System.IO.File.ReadAllText("Data/ProductSeedData.json");
                var products = JsonConvert.DeserializeObject<List<Product>>(productData);
                foreach (var product in products)
                {
                    _context.Products.Add(product);
                }

                _context.SaveChanges();
        }
              public void SeedLineItemData()
        {
                var lineItemData = System.IO.File.ReadAllText("Data/LineItemSeedData.json");
                var lineItems = JsonConvert.DeserializeObject<List<LineItem>>(lineItemData);
                foreach (var lineItem in lineItems)
                {
                    _context.LineItems.Add(lineItem);
                }

                _context.SaveChanges();
        }
              public void SeedOrderData()
        {
                var orderData = System.IO.File.ReadAllText("Data/OrderSeedData.json");
                var orders = JsonConvert.DeserializeObject<List<Order>>(orderData);
                foreach (var order in orders)
                {
                    _context.Orders.Add(order);
                }

                _context.SaveChanges();
        }


    }
}