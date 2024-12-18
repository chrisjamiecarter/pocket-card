using PocketCards.Domain.Entities;

namespace PocketCards.Domain.Services;

public interface IPocketPackService
{
    Task<bool> CreateAsync(PocketPack pack);
    Task<IReadOnlyList<PocketPack>> ReturnAllAsync();
    Task<PocketPack?> ReturnByIdAsync(Guid id);
    Task<PocketPack?> ReturnByNameAsync(string name);
}
