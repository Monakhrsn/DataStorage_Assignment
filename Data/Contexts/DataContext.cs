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
    public DbSet<ProjectEntity> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<CustomerEntity>().HasData(
            new CustomerEntity { Id = 2, CustomerName = "Rosa AB", CustomerEmail = "Rosa@gmail.com" },
            new CustomerEntity { Id = 3, CustomerName = "Anna Erikson", CustomerEmail = "Anna@gmail.com" },
            new CustomerEntity { Id = 4, CustomerName = "Liam AB", CustomerEmail = "Liam@gmail.com" },
            new CustomerEntity { Id = 5, CustomerName = "Mahi Roohbakhsh", CustomerEmail = "Mahi@gmail.com" }
            );
        
        
        modelBuilder.Entity<ProductEntity>().HasData(
            new ProductEntity { Id = 1, ProductName = "Consulting", Price = 1000},
            new ProductEntity { Id = 2, ProductName = "Education", Price = 2000 }
        );

        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId);
        
        modelBuilder.Entity<UserEntity>().HasData(
            new UserEntity() { Id = 1, UserFirstName = "Sara", UserLastName = "Mio", UserEmail = "Sara@gmail.com", RoleId = 1},
            new UserEntity() { Id = 2, UserFirstName = "John", UserLastName = "Doe", UserEmail = "John@gmail.com", RoleId = 2},
            new UserEntity() { Id = 3, UserFirstName = "Alex", UserLastName = "Peterson", UserEmail = "Alex@gmail.com", RoleId = 1},
            new UserEntity() { Id = 4, UserFirstName = "Peter", UserLastName = "Simpson", UserEmail = "Peter@gmail.com", RoleId = 2}
        );

        modelBuilder.Entity<RoleEntity>().HasData(
            new RoleEntity { Id = 1, RoleName = "Project manager" },
            new RoleEntity { Id = 2, RoleName = "Consultant" }
        );
        
        modelBuilder.Entity<StatusTypeEntity>().HasData(
            new StatusTypeEntity { Id = 1, StatusName = "Not Started" },
            new StatusTypeEntity { Id = 2,  StatusName = "In Progress" },
            new StatusTypeEntity { Id = 3, StatusName = "Completed" }
        );
    }
}