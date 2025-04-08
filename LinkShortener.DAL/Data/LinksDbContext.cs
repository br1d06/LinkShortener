using Microsoft.EntityFrameworkCore;
using LinkShortener.Domain.Models;

namespace LinkShortener.DAL.Data;

public class LinksDbContext : DbContext
{
    public LinksDbContext(DbContextOptions<LinksDbContext> options) : base(options)
    {

    }

    public DbSet<LinkInfo> Links { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LinkInfo>()
            .HasIndex(l => l.Id)
            .IsUnique();
    }
}

