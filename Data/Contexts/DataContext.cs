using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<CustomerEntity> Customers { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RoleEntity> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);

        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { Id = 1, RoleName = "Admin" },
            new RoleEntity { Id = 2, RoleName = "Customer" }
        );
        
        modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity { Id = 1, ProductName = "Product A", Price = 10.99m },
            new ProductEntity { Id = 2, ProductName = "Product B", Price = 20.99m },
            new ProductEntity { Id = 3, ProductName = "Product C", Price = 30.99m }
            );
        
        modelBuilder.Entity<StatusTypeEntity>().HasData(
            new StatusTypeEntity { Id = 1, StatusName = "Not Started" },
            new StatusTypeEntity { Id = 2,  StatusName = "In Progress" },
            new StatusTypeEntity { Id = 3, StatusName = "Completed" }
        );
    }
}