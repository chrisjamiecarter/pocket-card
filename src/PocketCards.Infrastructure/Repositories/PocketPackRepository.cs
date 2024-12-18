using Microsoft.EntityFrameworkCore;
using PocketCards.Application.Repositories;
using PocketCards.Domain.Entities;
using PocketCards.Infrastructure.Contexts;

namespace PocketCards.Infrastructure.Repositories;

internal class PocketPackRepository(PocketCardsDbContext context) : RepositoryBase<PocketPack>(context), IPocketPackRepository
{
    private readonly PocketCardsDbContext _context = context;

    public async Task<bool> CreateAsync(PocketPack pack)
    {
        await _context.PocketPacks.AddAsync(pack);

        var result = await SaveChangesAsync();
        return result > 0;
    }

    public async Task<PocketPack?> ReturnByNameAsync(string name)
    {
        return await _context.PocketPacks.SingleOrDefaultAsync(x => x.Name == name);
    }
}
