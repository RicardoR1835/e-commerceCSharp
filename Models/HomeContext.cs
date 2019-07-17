using Microsoft.EntityFrameworkCore;
 
namespace e_Commerce.Models
{
    public class HomeContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public HomeContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products {get;set;}
        public DbSet<Customer> Customers {get;set;}
        public DbSet<Order> Orders {get;set;}
    }
}