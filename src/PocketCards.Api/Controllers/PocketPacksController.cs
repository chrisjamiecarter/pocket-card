using Microsoft.AspNetCore.Mvc;
using PocketCards.Domain.Services;

namespace PocketCards.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PocketPacksController(IPocketPackService service) : ControllerBase
{
    [HttpGet(Name = nameof(GetPocketPacksAsync))]
    public async Task<IResult> GetPocketPacksAsync()
    {
        var entities = await service.ReturnAllAsync();

        return TypedResults.Ok(entities);
    }
}
