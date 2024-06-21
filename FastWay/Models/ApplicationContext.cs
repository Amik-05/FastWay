using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace FastWay.Models
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
            : base("name=ApplicationContext1")
        {
        }

        public virtual DbSet<AcceptOrder> AcceptOrder { get; set; }
        public virtual DbSet<Cargo> Cargo { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<DeliveryType> DeliveryType { get; set; }
        public virtual DbSet<Login> Login { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Subcategory> Subcategory { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<OrdersInProfile> OrdersInProfile { get; set; }
        public virtual DbSet<OrdersNonReg> OrdersNonReg { get; set; }
        public virtual DbSet<Cars> Cars { get; set; }
        public virtual DbSet<Movers> Movers { get; set; }
        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<MoversInOrder> MoversInOrder { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AcceptOrder>()
                .HasMany(e => e.Orders1)
                .WithOptional(e => e.AcceptOrder1)
                .HasForeignKey(e => e.IsAccepted);

            modelBuilder.Entity<Cargo>()
                .Property(e => e.OverallVolume)
                .HasPrecision(38, 1);

            modelBuilder.Entity<Cargo>()
                .Property(e => e.TotalWeight)
                .HasPrecision(38, 1);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Cargo)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.Category);

            modelBuilder.Entity<DeliveryType>()
                .HasMany(e => e.Orders)
                .WithOptional(e => e.DeliveryType1)
                .HasForeignKey(e => e.DeliveryType);

            modelBuilder.Entity<Orders>()
                .HasMany(e => e.AcceptOrder)
                .WithOptional(e => e.Orders)
                .HasForeignKey(e => e.AcceptOrderInfo);

            modelBuilder.Entity<Subcategory>()
                .HasMany(e => e.Cargo)
                .WithOptional(e => e.Subcategory1)
                .HasForeignKey(e => e.Subcategory);
        }
    }
}
