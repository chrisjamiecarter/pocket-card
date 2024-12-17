using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PocketCards.Domain.Entities;

namespace PocketCards.Infrastructure.Configurations;

internal class PocketCardConfiguration : IEntityTypeConfiguration<PocketCard>
{
    public void Configure(EntityTypeBuilder<PocketCard> builder)
    {
        builder.ToTable("PocketCard");

        builder.HasKey(pk => pk.Id);

        builder.Property(p => p.Number).IsRequired();
        builder.Property(p => p.Name).IsRequired();

        builder.Property(p => p.Rarity).HasConversion<int>();
        builder.Property(p => p.Type).HasConversion<int>();

        builder.HasOne(one => one.Pack)
               .WithMany(many => many.Cards)
               .HasForeignKey(fk => fk.PocketPackId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
