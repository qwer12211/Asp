using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Osipan.Models
{
    public partial class practosElectricASPnetContext : DbContext
    {
        public practosElectricASPnetContext()
        {
        }

        public practosElectricASPnetContext(DbContextOptions<practosElectricASPnetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Catalog> Catalogs { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<PosOrder> PosOrders { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-PJPTSCI\\SQLEXPRESS;Initial Catalog=practosElectricASPnet;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.IdCart)
                    .HasName("PK__Cart__72140ECFA2AC72CA");

                entity.ToTable("Cart");

                entity.Property(e => e.IdCart).HasColumnName("ID_Cart");

                entity.Property(e => e.CatalogId).HasColumnName("Catalog_ID");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UsersId).HasColumnName("Users_ID");

                entity.HasOne(d => d.Catalog)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.CatalogId)
                    .HasConstraintName("FK__Cart__Catalog_ID__59FA5E80");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK__Cart__Users_ID__5AEE82B9");
            });

            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasKey(e => e.IdCatalog)
                    .HasName("PK__Catalogs__38D620C57F1A87BE");

                entity.Property(e => e.IdCatalog).HasColumnName("ID_Catalog");

                entity.Property(e => e.CatalogDescription).HasMaxLength(255);

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Descriptions).HasMaxLength(100);

                entity.Property(e => e.Names).HasMaxLength(100);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Catalogs)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Catalogs__Catego__4D94879B");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__Orders__EC9FA955F9C67D28");

                entity.Property(e => e.IdOrder).HasColumnName("ID_Order");

                entity.Property(e => e.OrderDate).HasColumnType("datetime");

                entity.Property(e => e.TotalSum).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserID__534D60F1");
            });

            modelBuilder.Entity<PosOrder>(entity =>
            {
                entity.HasKey(e => e.IdPosOrder)
                    .HasName("PK__PosOrder__D77BC1CBC05C97C9");

                entity.ToTable("PosOrder");

                entity.Property(e => e.IdPosOrder).HasColumnName("ID_PosOrder");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.PosOrders)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__PosOrder__OrderI__5629CD9C");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PosOrders)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__PosOrder__Produc__571DF1D5");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__ProductC__6DB3A68A1A7E37C8");

                entity.ToTable("ProductCategory");

                entity.Property(e => e.IdCategory).HasColumnName("ID_Category");

                entity.Property(e => e.CategoryName).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PK__Roles__43DCD32D9C56ED3B");

                entity.Property(e => e.IdRole).HasColumnName("ID_Role");

                entity.Property(e => e.NameRole).HasMaxLength(25);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK__Users__ED4DE4428809305E");

                entity.Property(e => e.IdUser).HasColumnName("ID_User");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.MiddleName).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.UserLogin).HasMaxLength(50);

                entity.Property(e => e.UserPassword).HasMaxLength(64);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Users__RoleId__5070F446");

               
            });
            modelBuilder.Entity<Cart>()
               .HasOne(c => c.Catalog)
               .WithMany()
               .HasForeignKey(c => c.CatalogId);
           
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
