using Microsoft.EntityFrameworkCore;
using PocketCards.Domain.Entities;
using PocketCards.Infrastructure.Configurations;

namespace PocketCards.Infrastructure.Contexts;

internal class PocketCardsDbContext(DbContextOptions<PocketCardsDbContext> options) : DbContext(options)
{
    public DbSet<PocketCard> PocketCards { get; set; } = default!;

    public DbSet<PocketPack> PocketPacks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PocketCardConfiguration());
        modelBuilder.ApplyConfiguration(new PocketPackConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}
