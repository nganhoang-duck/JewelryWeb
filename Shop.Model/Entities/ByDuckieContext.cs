using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Shop.Model.Entities
{
    public partial class ByDuckieContext : DbContext
    {
        public ByDuckieContext()
        {
        }

        public ByDuckieContext(DbContextOptions<ByDuckieContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Footer> Footers { get; set; } = null!;
		public virtual DbSet<Customer> Customers { get; set; } = null!;

		public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuGroup> MenuGroups { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Page> Pages { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostCategory> PostCategories { get; set; } = null!;
        public virtual DbSet<PostTag> PostTags { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductTag> ProductTags { get; set; } = null!;
        public virtual DbSet<Slide> Slides { get; set; } = null!;
        public virtual DbSet<SupportOnline> SupportOnlines { get; set; } = null!;
        public virtual DbSet<Tag> Tags { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Info> Infos { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-FF9VATI;Database=ByDuckie;integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Footer>(entity =>
            {
                entity.Property(e => e.Id)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ID");
			});


			modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Target).HasMaxLength(10);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .HasColumnName("URL");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.GroupId)
                    .HasConstraintName("FK__Menus__GroupID__4F7CD00D");
            });

            modelBuilder.Entity<MenuGroup>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.CustomerAddress).HasMaxLength(250);

                entity.Property(e => e.CustomerEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerMobile)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName).HasMaxLength(250);

                entity.Property(e => e.PaymentMethod).HasMaxLength(250);

                entity.Property(e => e.PaymentStatus).HasMaxLength(50);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany()
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK__OrderDeta__Order__49C3F6B7");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__OrderDeta__Produ__4AB81AF0");
            });

            modelBuilder.Entity<Page>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.MetaDescription).HasMaxLength(250);

                entity.Property(e => e.MetaKeyWord).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Posts__CategoryI__3E52440B");
            });

            modelBuilder.Entity<PostCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<PostTag>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.TagId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TagID");

                entity.HasOne(d => d.Post)
                    .WithMany()
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostTags__PostID__44FF419A");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__PostTags__TagID__45F365D3");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.MoreImage).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Promotion).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__398D8EEE");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(250);
            });

            modelBuilder.Entity<ProductTag>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.TagId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TagID");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__ProductTa__Produ__4222D4EF");

                entity.HasOne(d => d.Tag)
                    .WithMany()
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK__ProductTa__TagID__4316F928");
            });

            modelBuilder.Entity<Slide>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Image).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .HasColumnName("URL");
            });

            modelBuilder.Entity<SupportOnline>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.Department).HasMaxLength(250);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Facebook)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Skype).HasMaxLength(250);
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
