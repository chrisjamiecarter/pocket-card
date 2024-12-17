namespace PocketCards.Domain.Entities;

public class PocketPack : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<PocketCard> Cards { get; set; } = [];
}
