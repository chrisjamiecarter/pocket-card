using PocketCards.Api.Contracts.Requests;
using PocketCards.Api.Contracts.Responses;
using PocketCards.Domain.Entities;

namespace PocketCards.Api.Contracts.Mappings;

public static class PocketPackMappings
{
    public static PocketPack ToDomain(this PocketPackCreateRequest request)
    {
        return new PocketPack
        {
            Id = Guid.CreateVersion7(),
            Name = request.Name,
        };
    }

    public static PocketPackResponse ToResponse(this PocketPack entity)
    {
        return new PocketPackResponse(entity.Id, entity.Name);
    }

    public static IReadOnlyList<PocketPackResponse> ToResponse(this IReadOnlyList<PocketPack> entities)
    {
        return entities.Select(x => x.ToResponse()).ToList();
    }
}
