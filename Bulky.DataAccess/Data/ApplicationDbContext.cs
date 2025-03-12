using Bulky.Models.Models;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext()
    {
        
    }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
        
    }
    
    // Required for EF Core CLI tools
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            // This is only used for design-time commands like migrations
            optionsBuilder.UseSqlServer("Server=DESKTOP-4BL2LOC;Database=Bulky;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Action", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Mobile", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Labtop", DisplayOrder = 3 }
        );
        
        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                Id = 1,
                Title = "Kite Runner",
                Description = "Description for Kite Runner",
                ISBN = "123456",
                Author = "Khaled Hosseini",
                ListPrice = 100,
                Price = 50,
                Price50 = 45,
                Price100 = 40,
                CategoryId = 1,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81K7c0q1CvL.jpg"
            },
            new Product
            {
                Id = 2,
                Title = "Kafka on the Shore",
                Description = "Description for Kafka on the Shore",
                ISBN = "123457",
                Author = "Haruki Murakami",
                ListPrice = 200,
                Price = 100,
                Price50 = 95,
                Price100 = 90,
                CategoryId = 2,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81K7c0q1CvL.jpg"
            },
            new Product
            {
                Id = 3,
                Title = "Sapiens",
                Description = "Description for Sapiens",
                ISBN = "123458",
                Author = "Yuval Noah Harari",
                ListPrice = 150,
                Price = 75,
                Price50 = 70,
                Price100 = 65,
                CategoryId = 3,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81K7c0q1CvL.jpg"
            },
            new Product
            {
                Id = 4,
                Title = "The Great Gatsby",
                Description = "Description for The Great Gatsby",
                ISBN = "123459",
                Author = "F. Scott Fitzgerald",
                ListPrice = 120,
                Price = 60,
                Price50 = 55,
                Price100 = 50,
                CategoryId = 1,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81K7c0q1CvL.jpg"
            },
            new Product
            {
                Id = 5,
                Title = "To Kill a Mockingbird",
                Description = "Description for To Kill a Mockingbird",
                ISBN = "123460",
                Author = "Harper Lee",
                ListPrice = 110,
                Price = 55,
                Price50 = 50,
                Price100 = 45,
                CategoryId = 2,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81K7c0q1CvL.jpg"
            },
            new Product
            {
                Id = 6,
                Title = "The Catcher in the Rye",
                Description = "Description for The Catcher in the Rye",
                ISBN = "123461",
                Author = "J.D. Salinger",
                ListPrice = 130,
                Price = 65,
                Price50 = 60,
                Price100 = 55,
                CategoryId = 3,
                ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/81K7c0q1CvL.jpg"
            }
        );
    }
    
    
}