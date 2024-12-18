using PocketCards.Domain.Entities;
using PocketCards.Domain.Enums;

namespace PocketCards.Application.Repositories;

public interface IPocketCardRepository
{
    Task<bool> CreateAsync(PocketCard card);
    Task<IReadOnlyList<PocketCard>> ReturnAsync();
    Task<PocketCard?> ReturnAsync(Guid id);
    Task<PocketCard?> ReturnByNumberAsync(string number);
    Task<IReadOnlyList<PocketCard>> ReturnByRarityAsync(PocketCardRarity rarity);
    Task<IReadOnlyList<PocketCard>> ReturnByTypeAsync(PocketCardType type);
    Task<bool> UpdateAsync(PocketCard card);
}
