using Microsoft.AspNetCore.Mvc;
using PocketCards.Domain.Services;

namespace PocketCards.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PocketCardsController(IPocketCardService service) : ControllerBase
{
    [HttpGet(Name = nameof(GetPocketCardsAsync))]
    public async Task<IResult> GetPocketCardsAsync()
    {
        var entities = await service.ReturnAllAsync();

        return TypedResults.Ok(entities);
    }
}
