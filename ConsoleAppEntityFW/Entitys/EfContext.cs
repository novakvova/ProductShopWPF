using ConsoleAppEntityFW.Migrations.Views.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppEntityFW.Entitys
{
    public class EfContext : DbContext
    {
        public EfContext() : base("ConnectionShopDB")
        {

        }
        #region Tables

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<FilterName> FilterNames { get; set; }

        public DbSet<FilterValue> FilterValues { get; set; }

        public DbSet<Filter> Filters { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<FilterNameGroups> FilterGroups { get; set; }

        public DbSet<ProductImage> ProductImages { get; set; }

        #endregion

        #region Views

        public DbSet<VFilterNameGroup> VFilterNameGroups { get; set; }

        #endregion
    }
}
