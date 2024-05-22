using Agc.GoodShepherd.Application.Queries.Sermons;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers.Api.V1;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/[action]")]
public class SermonsController:BaseController
{
    private readonly IMediator _mediator;

    public SermonsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] GetSermonsQuery query)
    {
        return CustomResponse(await _mediator.Send(query));
    }
    
}