using Microsoft.AspNetCore.Mvc;
using PocketCards.Api.Contracts.Mappings;
using PocketCards.Api.Contracts.Requests;
using PocketCards.Domain.Services;

namespace PocketCards.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PocketCardsController(IPocketCardService service) : ControllerBase
{
    [HttpGet("{id}", Name = nameof(GetPocketCardByIdAsync))]
    public async Task<IResult> GetPocketCardByIdAsync([FromRoute] Guid id)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    [HttpGet("number", Name = nameof(GetPocketCardByNumberAsync))]
    public async Task<IResult> GetPocketCardByNumberAsync([FromQuery] string number)
    {
        var entity = await service.ReturnByNumberAsync(number);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    [HttpGet(Name = nameof(GetAllPocketCardsAsync))]
    public async Task<IResult> GetAllPocketCardsAsync()
    {
        var entities = await service.ReturnAllAsync();

        return TypedResults.Ok(entities.ToResponse());
    }

    [HttpPost(Name = nameof(PostPocketCardAsync))]
    public async Task<IResult> PostPocketCardAsync([FromBody] PocketCardCreateRequest request)
    {
        var entity = request.ToDomain(service.GetImageFilePath(request.ImageFileName));

        var result = await service.CreateAsync(entity);

        return result
            ? TypedResults.CreatedAtRoute(entity.ToResponse(), nameof(GetPocketCardByIdAsync), new { id = entity.Id })
            : TypedResults.BadRequest(new { error = "Unable to POST pocket card." });
    }

    [HttpPut("{id}", Name = nameof(PutPocketCardAsync))]
    public async Task<IResult> PutPocketCardAsync([FromRoute] Guid id, [FromBody] PocketCardUpdateRequest request)
    {
        var entity = await service.ReturnByIdAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedEntity = request.ToDomain(entity.Id, service.GetImageFilePath(request.ImageFileName));

        var result = await service.UpdateAsync(updatedEntity);

        return result
            ? TypedResults.Ok(entity.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to PUT pocket card." });
    }
}
