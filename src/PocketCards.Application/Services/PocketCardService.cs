using Microsoft.Extensions.Options;
using PocketCards.Application.Constants;
using PocketCards.Application.Options;
using PocketCards.Application.Repositories;
using PocketCards.Domain.Entities;
using PocketCards.Domain.Enums;
using PocketCards.Domain.Services;

namespace PocketCards.Application.Services;

public class PocketCardService(IPocketCardRepository repository, IOptions<CdnOptions> cdnOptions) : IPocketCardService
{
    public async Task<bool> CreateAsync(PocketCard card)
    {
        card.ImageFilePath = GetValidImageFilePath(card.ImageFilePath);

        return await repository.CreateAsync(card);
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
        card.ImageFilePath = GetValidImageFilePath(card.ImageFilePath);

        return await repository.UpdateAsync(card);
    }

    private string GetValidImageFilePath(string imageFilePath)
    {
        return string.IsNullOrWhiteSpace(imageFilePath)
            ? Path.Combine(cdnOptions.Value.PocketCardImageHostUrl, DefaultValues.DefaultPocketCardImageFileName)
            : imageFilePath;
    }
}
