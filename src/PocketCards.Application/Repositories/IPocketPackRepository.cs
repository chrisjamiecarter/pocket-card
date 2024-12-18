using PocketCards.Domain.Entities;

namespace PocketCards.Application.Repositories;

public interface IPocketPackRepository
{
    Task<bool> CreateAsync(PocketPack pack);
    Task<IReadOnlyList<PocketPack>> ReturnAsync();
    Task<PocketPack?> ReturnAsync(Guid id);
    Task<PocketPack?> ReturnByNameAsync(string name);
}