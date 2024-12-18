using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using PocketCards.Domain.Entities;
using PocketCards.Infrastructure.Contexts;

namespace PocketCards.Infrastructure.Repositories;

internal abstract class RepositoryBase<T>(PocketCardsDbContext context) where T : BaseEntity
{
    public async Task<T?> ReturnAsync(Guid id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    protected async Task<int> SaveChangesAsync()
    {
        try
        {
            return await context.SaveChangesAsync();
        }
        catch (Exception exception)
        {
            Trace.TraceWarning(exception.Message);
            return -1;
        }
    }
}
