using Microsoft.EntityFrameworkCore;
using Shopimax.API.Models;

namespace Shopimax.API.Data
{
    public class DataContext: DbContext
    {
         public DataContext(DbContextOptions<DataContext>  options) : base (options) {}

         public DbSet<Shop> Shops { get; set; }
         public DbSet<Product> Products { get; set; }
         public DbSet<LineItem> LineItems { get; set; }
         public DbSet<Order> Orders { get; set; }
         public DbSet<User> Users {get; set;}

        
        
    }
}