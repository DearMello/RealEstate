using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Agent> Agents => Set<Agent>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Listing> Listings => Set<Listing>();
    public DbSet<Inquiry> Inquiries => Set<Inquiry>();
    public DbSet<AppUser> Users => Set<AppUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Property>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Title).IsRequired().HasMaxLength(200);
            e.Property(x => x.City).IsRequired().HasMaxLength(100);
            e.Property(x => x.Country).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Agent>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(x => x.Email).IsUnique();
            e.Property(x => x.LicenseNumber).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Client>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(x => x.Email).IsUnique();
        });

        modelBuilder.Entity<Listing>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Price).HasColumnType("decimal(18,2)");
            e.HasOne(x => x.Property)
                .WithMany(p => p.Listings)
                .HasForeignKey(x => x.PropertyId)
                .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Agent)
                .WithMany(a => a.Listings)
                .HasForeignKey(x => x.AgentId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Inquiry>(e =>
        {
            e.HasKey(x => x.Id);
            e.HasOne(x => x.Listing)
                .WithMany(l => l.Inquiries)
                .HasForeignKey(x => x.ListingId)
                .OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Client)
                .WithMany(c => c.Inquiries)
                .HasForeignKey(x => x.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AppUser>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Email).IsRequired().HasMaxLength(200);
            e.HasIndex(x => x.Email).IsUnique();
        });
    }
}
