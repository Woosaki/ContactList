using ContactListAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactListAPI.Data;

public class ContactListDbContext(DbContextOptions<ContactListDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Subcategory> Subcategories { get; set; }
    public DbSet<Contact> Contacts { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();
        modelBuilder.Entity<Category>().HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<Subcategory>().HasIndex(s => s.Name).IsUnique();

        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Business" },
            new Category { Id = 2, Name = "Private" },
            new Category { Id = 3, Name = "Other" });


        modelBuilder.Entity<Subcategory>().HasData(
            new Subcategory { Id = 1, Name = "Boss", CategoryId = 1 },
            new Subcategory { Id = 2, Name = "Client", CategoryId = 1 },
            new Subcategory { Id = 3, Name = "Co-worker", CategoryId = 1 });
    }
}
