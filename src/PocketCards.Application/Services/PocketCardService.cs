using PocketCards.Application.Repositories;
using PocketCards.Domain.Entities;
using PocketCards.Domain.Enums;
using PocketCards.Domain.Services;

namespace PocketCards.Application.Services;

public class PocketCardService(IPocketCardRepository repository) : IPocketCardService
{
    public async Task<bool> CreateAsync(PocketCard card)
    {
        return await repository.CreateAsync(card);
    }

    public async Task<bool> DeleteAsync(PocketCard card)
    {
        return await repository.DeleteAsync(card);
    }

    public async Task<IReadOnlyList<PocketCard>> ReturnAllAsync()
    {
        return await repository.ReturnAsync();
    }

    public async Task<PocketCard?> ReturnByIdAsync(Guid id)
    {
        return await repository.ReturnAsync(id);
    }

    public async Task<IReadOnlyList<PocketCard>> ReturnByRarityAsync(PocketCardRarity rarity)
    {
        return await repository.ReturnByRarityAsync(rarity);
    }

    public async Task<IReadOnlyList<PocketCard>> ReturnByTypeAsync(PocketCardType type)
    {
        return await repository.ReturnByTypeAsync(type);
    }

    public async Task<bool> UpdateAsync(PocketCard card)
    {
        return await repository.UpdateAsync(card);
    }
}
