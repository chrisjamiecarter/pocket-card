using PocketCards.Domain.Enums;

namespace PocketCards.Api.Contracts.Requests;

public record PocketCardUpdateRequest
{
    public PocketCardUpdateRequest(string number, string name, string imageFileName, PocketCardRarity rarity, PocketCardType type, int? hitPoints, PocketCardStage? stage, int? packPoints, bool isCollected, Guid pockePackId)
    {
        Number = number;
        Name = name;
        ImageFileName = imageFileName;
        Rarity = rarity;
        Type = type;
        HitPoints = hitPoints;
        Stage = stage;
        PackPoints = packPoints;
        IsCollected = isCollected;
        PocketPackId = pockePackId;
    }

    public string Number { get; private set; }

    public string Name { get; private set; }

    public string ImageFileName { get; private set; } = string.Empty;

    public PocketCardRarity Rarity { get; private set; } = PocketCardRarity.Unknown;

    public PocketCardType Type { get; private set; } = PocketCardType.Unknown;

    public int? HitPoints { get; private set; }

    public PocketCardStage? Stage { get; private set; }

    public int? PackPoints { get; private set; }

    public bool IsCollected { get; private set; } = false;

    public Guid PocketPackId { get; private set; }

}
