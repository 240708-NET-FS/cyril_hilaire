using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

namespace ContactBook.Entities;

public class AppDbContext : DbContext
{
    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Phone> Phones { get; set; }

    public AppDbContext(DbContextOptions options) : base(options){}

    public AppDbContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if(!optionsBuilder.IsConfigured)
        {
            // Console.WriteLine(Directory.GetCurrentDirectory() + "appsettings.json");
            IConfigurationRoot config = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json")
                                            .Build();   

            var connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Contact>()
            .HasMany(e => e.Phones)
            .WithOne(e => e.Contacts)
            .HasForeignKey(e => e.ContactId);
    }
}