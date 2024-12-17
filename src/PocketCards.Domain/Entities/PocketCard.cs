using PocketCards.Domain.Enums;

namespace PocketCards.Domain.Entities;

public class PocketCard : BaseEntity
{
    public required string Number { get; set; }

    public required string Name { get; set; }

    public string ImageFilePath { get; set; } = string.Empty;

    public PocketCardRarity Rarity { get; set; } = PocketCardRarity.Unknown;

    public PocketCardType Type { get; set; } = PocketCardType.Unknown;

    public int? HitPoints { get; set; }

    public PocketCardStage? Stage { get; set; }

    public int? PackPoints { get; set; }

    public bool IsCollected { get; set; } = false;

    public required Guid PocketPackId { get; set; }

    public PocketPack? Pack { get; set; }
}
