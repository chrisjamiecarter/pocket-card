using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PocketCards.Domain.Entities;

namespace PocketCards.Infrastructure.Configurations;

internal class PocketPackConfiguration : IEntityTypeConfiguration<PocketPack>
{
    public void Configure(EntityTypeBuilder<PocketPack> builder)
    {
        builder.ToTable("PocketPack");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Name).IsRequired();
    }
}
