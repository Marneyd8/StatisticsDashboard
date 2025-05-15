using Microsoft.EntityFrameworkCore;
using StatisticsDashboard.Models;

namespace StatisticsDashboard.Data
{
    public class MyAppContext : DbContext
    {
        // defines the database context (entity relationships, seeds initial data)
        // var _context is used as a dependency injection in controllers and services to interact with the database
        // _context is injected in contructor
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // seeding
            modelBuilder.Entity<ItemClient>().HasKey(ic => new
            {
                ic.ItemId,
                ic.ClientId
            });

            modelBuilder.Entity<ItemClient>().HasOne(i => i.Item).WithMany(ic => ic.ItemClients).HasForeignKey(i => i.ItemId);
            modelBuilder.Entity<ItemClient>().HasOne(i => i.Client).WithMany(ic => ic.ItemClients).HasForeignKey(i => i.ClientId);


            modelBuilder.Entity<Item>().HasData(
                new Item { Id = 4, Name = "microphone", Price = 40 }
                );
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics"},
                new Category { Id = 2, Name = "Books" }
                );

            base.OnModelCreating(modelBuilder);
        }
        // relationships (from Models)
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ItemClient> ItemClients { get; set; }
    }
}
