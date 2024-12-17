using PocketCards.Application.Repositories;
using PocketCards.Domain.Entities;
using PocketCards.Domain.Services;

namespace PocketCards.Application.Services;

public class PocketPackService(IPocketPackRepository repository) : IPocketPackService
{
    public async Task<bool> CreateAsync(PocketPack pack)
    {
        return await repository.CreateAsync(pack);
    }

    public async Task<IReadOnlyList<PocketPack>> ReturnAllAsync()
    {
        return await repository.ReturnAsync();
    }

    public async Task<PocketPack?> ReturnByIdAsync(Guid id)
    {
        return await repository.ReturnAsync(id);
    }
}
