using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace EF_session.Db
{
    public class StoreDbContext : DbContext
    {
        public DbSet<Models.Customer> Customers { set; get; }
        public DbSet<Models.Category> Categories { set; get; }
        public DbSet<Models.Order> Orders { set; get; }
        public DbSet<Models.OrderItem> OrderItems { set; get; }
        public DbSet<Models.Product> Products { set; get; }
        public DbSet<Models.Review> Reviews { set; get; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
           optionsBuilder.UseSqlServer(@"Server=localhost;Database=OnlineStoreDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }


    }
}
