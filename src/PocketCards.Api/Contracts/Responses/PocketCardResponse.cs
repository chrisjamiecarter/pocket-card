using PocketCards.Domain.Enums;

namespace PocketCards.Api.Contracts.Responses;

public record PocketCardResponse(Guid Id, string Number, string Name, string ImageFilePath, PocketCardRarity Rarity, PocketCardType Type, int? HitPoints, PocketCardStage? Stage, int? PackPoints, bool IsCollected, Guid PocketPackId);
