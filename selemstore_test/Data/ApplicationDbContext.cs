using Microsoft.EntityFrameworkCore;
using selemstore_test.Models;

namespace selemstore_test.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ProductSizeColor> ProductSizeColors { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // علاقة CartItem مع Product
        //    modelBuilder.Entity<CartItem>()
        //        .HasOne(ci => ci.Product)
        //        .WithMany()
        //        .HasForeignKey(ci => ci.ProductId)
        //        .OnDelete(DeleteBehavior.Restrict);

        //    // علاقة Product مع Category
        //    modelBuilder.Entity<Product>()
        //        .HasOne(p => p.Category)
        //        .WithMany(c => c.Products)
        //        .HasForeignKey(p => p.CategoryId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // علاقة ProductImage مع Product
        //    modelBuilder.Entity<ProductImage>()
        //        .HasOne(pi => pi.Product)
        //        .WithMany(p => p.ProductImages)
        //        .HasForeignKey(pi => pi.ProductId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // إعداد المفتاح المركب لجدول ProductSizeColor
        //    modelBuilder.Entity<ProductSizeColor>()
        //        .HasKey(psc => new { psc.ProductId, psc.SizeId, psc.ColorId });

        //    modelBuilder.Entity<ProductSizeColor>()
        //        .HasOne(psc => psc.Product)
        //        .WithMany(p => p.ProductSizeColors)
        //        .HasForeignKey(psc => psc.ProductId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<ProductSizeColor>()
        //        .HasOne(psc => psc.Size)
        //        .WithMany(s => s.ProductSizeColors) // ✅ التعديل الصحيح هنا
        //        .HasForeignKey(psc => psc.SizeId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<ProductSizeColor>()
        //        .HasOne(psc => psc.Color)
        //        .WithMany(c => c.ProductSizeColors)
        //        .HasForeignKey(psc => psc.ColorId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // علاقة OrderDetail مع Order و Product
        //    modelBuilder.Entity<OrderDetail>()
        //        .HasOne(od => od.Order)
        //        .WithMany(o => o.OrderDetails)
        //        .HasForeignKey(od => od.OrderId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    modelBuilder.Entity<OrderDetail>()
        //        .HasOne(od => od.Product)
        //        .WithMany()
        //        .HasForeignKey(od => od.ProductId)
        //        .OnDelete(DeleteBehavior.Restrict);
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // إعداد المفتاح المركب لجدول ProductSizeColor
            modelBuilder.Entity<ProductSizeColor>()
                .HasKey(psc => new { psc.ProductId, psc.SizeId, psc.ColorId });

            modelBuilder.Entity<ProductSizeColor>()
                .HasOne(psc => psc.Product)
                .WithMany(p => p.ProductSizeColors)
                .HasForeignKey(psc => psc.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSizeColor>()
                .HasOne(psc => psc.Size)
                .WithMany(s => s.ProductSizeColors)
                .HasForeignKey(psc => psc.SizeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSizeColor>()
                .HasOne(psc => psc.Color)
                .WithMany(c => c.ProductSizeColors)
                .HasForeignKey(psc => psc.ColorId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ تعريف الحقول الجديدة مع قيم افتراضية
            modelBuilder.Entity<ProductSizeColor>()
                .Property(psc => psc.CostPrice)
                .HasDefaultValue(0); // السعر الافتراضي للشراء

            modelBuilder.Entity<ProductSizeColor>()
                .Property(psc => psc.SellingPrice)
                .HasDefaultValue(0); // السعر الافتراضي للبيع

            // علاقة CartItem مع Product
            modelBuilder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // علاقة Product مع Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة ProductImage مع Product
            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة OrderDetail مع Order و Product
            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderDetail>()
                .HasOne(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}