using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Assignment2.Models;
using Assignment2.Models.DataModel;
using System.Runtime.Serialization;
using Microsoft.AspNetCore.Identity;

namespace Assignment2.Data
{
    [DataContract]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        [DataMember]
        public DbSet<OwnerInventory> OwnerInventories { get; set; }

        [DataMember]
        public DbSet<Product> Products { get; set; }

        [DataMember]
        public DbSet<StockRequest> StockRequests { get; set; }

        [DataMember]
        public DbSet<Store> Stores { get; set; }

        [DataMember]
        public DbSet<StoreInventory> StoreInventories { get; set; }

        [DataMember]
        public DbSet<Cart> Carts { get; set; }

        [DataMember]
        public DbSet<Order> Orders { get; set; }

        //[DataMember]
        //public DbSet<IdentityRole> Roles { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<StoreInventory>()
                .HasKey(c => new { c.StoreID, c.ProductID });

            builder.Entity<Cart>()
                .HasKey(c => new { c.CustomerID, c.ProductID, c.StoreID });
        }
    }
}
