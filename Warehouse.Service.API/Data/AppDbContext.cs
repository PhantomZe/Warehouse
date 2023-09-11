using Microsoft.EntityFrameworkCore;
using Warehouse.Service.API.Models;

namespace Warehouse.Service.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Item> items { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<TransactionItem> transaction_items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<Item>().HasData(new Item
            {
                ItemID = 1,
                ItemName = "Item A",
                ItemAmount = 10,
                Status = 1,
                LastUpdated = DateTime.Now,
            });

            modelBuilder.Entity<Item>().HasData(new Item
            {
                ItemID = 2,
                ItemName = "Item B",
                ItemAmount = 20,
                Status = 1,
                LastUpdated = DateTime.Now,
            });
        }
    }
}
