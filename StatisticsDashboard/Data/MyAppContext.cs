using Microsoft.EntityFrameworkCore;
using StatisticsDashboard.Models;

namespace StatisticsDashboard.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<ItemClient> ItemClients { get; set; }
    }
}
