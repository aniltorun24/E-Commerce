using e_CommerceApi.Models.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace e_CommerceApi.Models.Context
{
    public class Context : IdentityDbContext<AppUser, AppRole, string>
    {
        public Context(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }  = null!;

        public DbSet<Cart> Carts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(
                new List<Product>
                {
                    new Product
                    {
                        Id = 1,
                        Name = "Iphone 15",
                        Description = "Telefon Açıklaması.",
                        Price = 70000,
                        IsActive = true,
                        ImageUrl = "1.jpg",
                        Stock = 100
                    },
                    new Product
                    {
                        Id = 2,
                        Name = "Iphone 16",
                        Description = "Telefon Açıklaması.",
                        Price = 90000,
                        IsActive = true,
                        ImageUrl =  "2.jpg",
                        Stock = 50
                    },
                    new Product
                    {
                        Id = 3,
                        Name = "Iphone 17",
                        Description = "Telefon Açıklaması.",
                        Price = 60000,
                        IsActive = true,
                        ImageUrl =  "3.jpg",
                        Stock = 70
                    }
                }
                );
        }
    }
}
