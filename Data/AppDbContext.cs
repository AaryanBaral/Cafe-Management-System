using Cafe_Management_System.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cafe_Management_System.Data;

public class AppDbContext (DbContextOptions<AppDbContext> options): IdentityDbContext<Users>(options)
{
    public DbSet<Admins> Admins { get; set; }
    public DbSet<Users> AppUsers { get; set; }
    public DbSet<Categories> Categories { get; set; }
    public DbSet<Orders> Orders { get; set; }
    public DbSet<OrderItems> OrderItems { get; set; }
    public DbSet<Ratings> Ratings { get; set; }
    public DbSet<Payments> Payments { get; set; }
    public DbSet<MenuItems> MenuItems { get; set; }
    public DbSet<Tables> Tables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // For user
        builder.Entity<Users>(entity =>
        {
            entity.Property(u => u.PasswordHash).IsRequired(false);
            
            entity.Property(u => u.ImageUrl)
                .HasMaxLength(255);      
            entity.Property(u => u.AuthId)
                .HasMaxLength(255);
            entity.Property(u => u.AuthType)
                .HasConversion<int>()
                .IsRequired();
        });

        // For Tables
        builder.Entity<Tables>(entity =>
        {
            entity.HasKey(c => c.TableId);

            entity.Property(t => t.TableNumber)
                .HasMaxLength(3);
        });

        // For Ratings
        builder.Entity<Ratings>(entity =>
        {
            entity.HasKey(c => c.RatingId);
 

            entity.HasOne(r => r.Customer)
                .WithMany()
                .HasForeignKey(r => r.CustomerId);


            entity.HasOne(r => r.MenuItem)
                .WithMany()
                .HasForeignKey(r => r.MenuItemId);


            entity.HasOne(r => r.Order)
                .WithMany()
                .HasForeignKey(r => r.OrderId);

        });

        // For Payments
        builder.Entity<Payments>(entity =>
        {
            entity.HasKey(c => c.PaymentId);
 

            entity.Property(p => p.Amount)
                .HasPrecision(28, 10);

            entity.HasOne(p => p.Order)
                .WithOne(o => o.Payment)
                .HasForeignKey<Payments>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
   

            entity.Property(p => p.PaymentStatus)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(p => p.PaymentMethod)
                .HasConversion<int>()
                .IsRequired();
        });

        // For Orders
        builder.Entity<Orders>(entity =>
        {
            entity.HasKey(c => c.OrderId);


            entity.HasOne(o => o.Table)
                .WithMany()
                .HasForeignKey(o => o.TableId)
                .OnDelete(DeleteBehavior.NoAction);
 

            entity.HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

            entity.Property(o => o.PaymentMethod)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(o => o.PaymentStatus)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(o => o.OrderStatus)
                .HasConversion<int>()
                .IsRequired();

            entity.HasOne(o => o.Payment) // Payment navigation property in Orders
                .WithOne(p => p.Order) // Order navigation property in Payments
                .OnDelete(DeleteBehavior.Cascade);


            entity.Property(o => o.TotalAmount)
                .HasPrecision(28, 10);
        });

        builder.Entity<Categories>(entity =>
        {
            entity.HasKey(c => c.CategoryId);


            entity.Property(u => u.CategoryDescription)
                .HasMaxLength(255);
            entity.Property(u => u.CategoryName)
                .HasMaxLength(255);
        });
        
        builder.Entity<Admins>(entity =>
        {
            entity.Property(a => a.ImageUrl)
                .HasMaxLength(255);
        });
        
        builder.Entity<MenuItems>(entity =>
        {
            entity.HasKey(c => c.MenuItemId);


            entity.HasOne(m => m.Category)
                .WithMany()
                .HasForeignKey(m => m.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

     
            entity.Property(m => m.SellingPrice)
                .HasPrecision(28, 10);

            entity.Property(m => m.CostPrice)
                .HasPrecision(28, 10);
            entity.Property(m => m.Name)
                .HasMaxLength(255);
            entity.Property(m => m.ImageUrl)
                .HasMaxLength(255);
            entity.Property(m => m.Description)
                .HasMaxLength(255);
            
            entity.Property( m => m.Spicy)
                .HasConversion<int>()
                .IsRequired();
        });
        
        builder.Entity<OrderItems>(entity =>
        {
            entity.HasKey(c => c.OrderItemId);


            entity.HasOne(o => o.Order)
                .WithMany()
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.NoAction);


            entity.HasOne(o => o.MenuItem)
                .WithMany()
                .HasForeignKey(o => o.MenuItemId)
                .OnDelete(DeleteBehavior.NoAction);


            entity.Property(o => o.UnitPrice)
                .HasPrecision(28, 10);

            entity.Property(o => o.SubTotal)
                .HasPrecision(28, 10);
        });
        
    }
}
