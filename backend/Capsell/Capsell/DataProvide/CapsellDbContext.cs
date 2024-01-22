
using System;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Capsell.DataProvide
{
    public class CapsellDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public CapsellDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public CapsellDbContext(IConfiguration configuration,
            DbContextOptions<CapsellDbContext> options) : base(options)
        {
            _configuration = configuration;
        }


        public virtual DbSet<Product>? Products { get; set; }
        public virtual DbSet<Shop>? Shops { get; set; }
        public virtual DbSet<Cart>? Carts { get; set; }
        public virtual DbSet<ShopCart>? ShopCarts { get; set; }
        public virtual DbSet<Order>? Orders { get; set; }
        public virtual DbSet<ShopOrder>? ShopOrders { get; set; }
        public virtual DbSet<Company>? Companies { get; set; }
        public virtual DbSet<CompaniesProduct>? CompaniesProducts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("ConnStr"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PhotoUrl).HasColumnName("photoUrl").HasMaxLength(999); ;

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.Category).HasColumnName("category");


                entity.HasOne(p => p.Shop)
                .WithMany(d => d.Products)
                .HasForeignKey(p => p.ShopId);

            });

            builder.Entity<CompaniesProduct>(entity =>
            {
                entity.ToTable("companiesProduct");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PhotoUrl).HasColumnName("photoUrl").HasMaxLength(999); ;

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.CompanyId).HasColumnName("companyId");

                entity.Property(e => e.Category).HasColumnName("category");


                entity.HasOne(p => p.Company)
                .WithMany(d => d.CompaniesProducts)
                .HasForeignKey(p => p.CompanyId);

            });

            builder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.ProductId).HasColumnName("productId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.ShopId).HasColumnName("shopId");


                entity.HasOne(p => p.Product)
                .WithMany(d => d.CartItems)
                .HasForeignKey(p => p.ProductId);

                entity.HasOne(p => p.User)
                .WithMany(d => d.CartItems)
                .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.Shop)
                .WithMany(d => d.CartItems)
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<ShopCart>(entity =>
            {
                entity.ToTable("shopCart");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.ProductId).HasColumnName("CompanyProductId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CompanyId).HasColumnName("companyId");


                entity.HasOne(p => p.Product)
                .WithMany(d => d.CartItems)
                .HasForeignKey(p => p.ProductId);

                entity.HasOne(p => p.User)
                .WithMany(d => d.ShopCartItems)
                .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.Company)
                .WithMany(d => d.CartItems)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Products).HasColumnName("products");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.ShopId).HasColumnName("shopId");


                entity.HasOne(p => p.User)
                .WithMany(d => d.Orders)
                .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.Shop)
                .WithMany(d => d.Orders)
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<ShopOrder>(entity =>
            {
                entity.ToTable("ShopOrder");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Products).HasColumnName("CompanyProducts");

                entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.Property(e => e.CompanyId).HasColumnName("companyId");


                entity.HasOne(p => p.User)
                .WithMany(d => d.ShopOrders)
                .HasForeignKey(p => p.UserId);

                entity.HasOne(p => p.Company)
                .WithMany(d => d.Orders)
                .HasForeignKey(p => p.CompanyId)
                .OnDelete(DeleteBehavior.NoAction);

            });

            builder.Entity<Shop>(entity =>
            {
                entity.ToTable("shop");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PhotoUrl).HasColumnName("photoUrl").HasMaxLength(999); ;

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.RegistrationLicenseNumber).HasColumnName("licenseNumber");

                entity.Property(e => e.UserId).HasColumnName("userId");

            });

            builder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id").IsRequired().ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.PhotoUrl).HasColumnName("photoUrl").HasMaxLength(999); ;

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LicenseNumber).HasColumnName("licenseNumber");

                entity.Property(e => e.UserId).HasColumnName("userId");

            });
        }
    }
}

