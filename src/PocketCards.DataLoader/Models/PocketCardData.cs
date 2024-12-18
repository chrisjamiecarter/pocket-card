using PocketCards.Domain.Enums;

namespace PocketCards.DataLoader.Models;

internal record PocketCardData(string Number, string Name, string ImageFileName, PocketCardRarity Rarity, string Pack, PocketCardType Type, int? HitPoints, PocketCardStage? Stage, int? PackPoints);
