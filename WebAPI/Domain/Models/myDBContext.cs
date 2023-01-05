using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain.Models
{
    public partial class myDBContext : DbContext
    {
        public myDBContext()
        {
        }

        public myDBContext(DbContextOptions<myDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CardItem> CardItems { get; set; } = null!;
        public virtual DbSet<Cart> Carts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<ProductTag> ProductTags { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=saledb; Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardItem>(entity =>
            {
                entity.ToTable("CardItem");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CartId).HasColumnName("CartID");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Discount)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Note)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Cart)
                    .WithMany(p => p.CardItems)
                    .HasForeignKey(d => d.CartId)
                    .HasConstraintName("FK_CardItem_Cart");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CardItems)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_CardItem_Product");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("Cart");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.City)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Country)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Mobile)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Note)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Province)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.SessionId)
                    .HasMaxLength(10)
                    .HasColumnName("SessionID")
                    .IsFixedLength();

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Note)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Slug)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Comment");

                entity.Property(e => e.Comment1)
                    .HasMaxLength(10)
                    .HasColumnName("Comment")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .HasColumnName("ID")
                    .IsFixedLength();

                entity.Property(e => e.ModifiedAt)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ParentId)
                    .HasMaxLength(10)
                    .HasColumnName("ParentID")
                    .IsFixedLength();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Rating)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Title)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Comment_Product");

                entity.HasOne(d => d.UserCreatedNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.UserCreated)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Customer");

                entity.Property(e => e.Address)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Number)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Employee");

                entity.Property(e => e.BirthDay)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(10)
                    .HasColumnName("UserID")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("Menu");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Slug)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Discount)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PublishedAt).HasColumnType("datetime");

                entity.Property(e => e.Quantity)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Slug)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.MenuId)
                    .HasConstraintName("FK_Product_Menu");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.ToTable("ProductImage");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImages)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductImage_Product");
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.ToTable("ProductTag");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.TagId).HasColumnName("TagID");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.ProductTags)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_ProductTag_Tag");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.ToTable("Tag");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Tag1)
                    .HasMaxLength(10)
                    .HasColumnName("Tag")
                    .IsFixedLength();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .IsFixedLength();

                entity.Property(e => e.Avatar)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.FirstName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Intro)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.LastLogin).HasColumnType("datetime");

                entity.Property(e => e.LastName)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.Profile)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.RegisteredAt).HasColumnType("datetime");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
