using Microsoft.AspNetCore.Mvc;
using PocketCards.Api.Contracts.Mappings;
using PocketCards.Api.Contracts.Requests;
using PocketCards.Domain.Services;

namespace PocketCards.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PocketPacksController(IPocketPackService service) : ControllerBase
{
    [HttpGet("{id}", Name = nameof(GetPocketPackByIdAsync))]
    public async Task<IResult> GetPocketPackByIdAsync([FromRoute] Guid id)
    {
        var entity = await service.ReturnByIdAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    [HttpGet("name", Name = nameof(GetPocketPackByNameAsync))]
    public async Task<IResult> GetPocketPackByNameAsync([FromQuery] string name)
    {
        var entity = await service.ReturnByNameAsync(name);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    [HttpGet(Name = nameof(GetAllPocketPacksAsync))]
    public async Task<IResult> GetAllPocketPacksAsync()
    {
        var entities = await service.ReturnAllAsync();

        return TypedResults.Ok(entities.ToResponse());
    }

    [HttpPost(Name = nameof(PostPocketPackAsync))]
    public async Task<IResult> PostPocketPackAsync([FromBody] PocketPackCreateRequest request)
    {
        var entity = request.ToDomain();

        var result = await service.CreateAsync(entity);

        return result
            ? TypedResults.CreatedAtRoute(entity.ToResponse(), nameof(GetPocketPackByIdAsync), new { id = entity.Id })
            : TypedResults.BadRequest(new { error = "Unable to post pocket pack." });
    }
}
