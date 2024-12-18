using Microsoft.EntityFrameworkCore;
using PocketCards.Application.Repositories;
using PocketCards.Domain.Entities;
using PocketCards.Domain.Enums;
using PocketCards.Infrastructure.Contexts;

namespace PocketCards.Infrastructure.Repositories;

internal class PocketCardRepository(PocketCardsDbContext context) : RepositoryBase<PocketCard>(context), IPocketCardRepository
{
    private readonly PocketCardsDbContext _context = context;

    public async Task<bool> CreateAsync(PocketCard card)
    {
        await _context.PocketCards.AddAsync(card);

        var result = await SaveChangesAsync();
        return result > 0;
    }

    public async Task<IReadOnlyList<PocketCard>> ReturnAsync()
    {
        return await _context.PocketCards.OrderBy(x => x.Number).ToListAsync();
    }

    public async Task<PocketCard?> ReturnByNumberAsync(string number)
    {
        return await _context.PocketCards.SingleOrDefaultAsync(x => x.Number == number);
    }

    public async Task<IReadOnlyList<PocketCard>> ReturnByRarityAsync(PocketCardRarity rarity)
    {
        return await _context.PocketCards.Where(x => x.Rarity == rarity).OrderBy(x => x.Number).ToListAsync();
    }

    public async Task<IReadOnlyList<PocketCard>> ReturnByTypeAsync(PocketCardType type)
    {
        return await _context.PocketCards.Where(x => x.Type == type).OrderBy(x => x.Number).ToListAsync();
    }

    public async Task<bool> UpdateAsync(PocketCard card)
    {
        var entity = await _context.PocketCards.FindAsync(card.Id);
        if (entity is not null)
        {
            _context.PocketCards.Entry(entity).CurrentValues.SetValues(card);
        }

        var result = await SaveChangesAsync();
        return result > 0;
    }
}
