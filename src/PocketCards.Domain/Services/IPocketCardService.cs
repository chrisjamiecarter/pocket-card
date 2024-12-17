using PocketCards.Domain.Entities;
using PocketCards.Domain.Enums;

namespace PocketCards.Domain.Services;

public interface IPocketCardService
{
    Task<bool> CreateAsync(PocketCard card);
    Task<bool> DeleteAsync(PocketCard card);
    Task<IReadOnlyList<PocketCard>> ReturnAllAsync();
    Task<PocketCard?> ReturnByIdAsync(Guid id);
    Task<IReadOnlyList<PocketCard>> ReturnByRarityAsync(PocketCardRarity rarity);
    Task<IReadOnlyList<PocketCard>> ReturnByTypeAsync(PocketCardType type);
    Task<bool> UpdateAsync(PocketCard card);
}
