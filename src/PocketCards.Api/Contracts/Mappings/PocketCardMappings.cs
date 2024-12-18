using PocketCards.Api.Contracts.Requests;
using PocketCards.Api.Contracts.Responses;
using PocketCards.Domain.Entities;

namespace PocketCards.Api.Contracts.Mappings;

public static class PocketCardMappings
{
    public static PocketCard ToDomain(this PocketCardCreateRequest request, string imageFilePath)
    {
        return new PocketCard
        {
            Id = Guid.CreateVersion7(),
            Number = request.Number,
            Name = request.Name,
            ImageFilePath = imageFilePath,
            Rarity = request.Rarity,
            Type = request.Type,
            HitPoints = request.HitPoints,
            Stage = request.Stage,
            PackPoints = request.PackPoints,
            PocketPackId = request.PocketPackId,
        };
    }

    public static PocketCard ToDomain(this PocketCardUpdateRequest request, Guid id, string imageFilePath)
    {
        return new PocketCard
        {
            Id = id,
            Number = request.Number,
            Name = request.Name,
            ImageFilePath = imageFilePath,
            Rarity = request.Rarity,
            Type = request.Type,
            HitPoints = request.HitPoints,
            Stage = request.Stage,
            PackPoints = request.PackPoints,
            PocketPackId = request.PocketPackId,
        };
    }

    public static PocketCardResponse ToResponse(this PocketCard entity)
    {
        return new PocketCardResponse(entity.Id, entity.Number, entity.Name, entity.ImageFilePath, entity.Rarity, entity.Type, entity.HitPoints, entity.Stage, entity.PackPoints, entity.IsCollected, entity.PocketPackId);
    }

    public static IReadOnlyList<PocketCardResponse> ToResponse(this IReadOnlyList<PocketCard> entities)
    {
        return entities.Select(x => x.ToResponse()).ToList();
    }
}
