using Agc.GoodShepherd.Application.Commands.Congregants;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Agc.GoodShepherd.Api.Controllers.V1;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/[action]")]
public class CongregantsController : BaseController
{
    private readonly IMediator _mediator;

    public CongregantsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AddCongregantCommand command)
    {
        return CustomResponse(await _mediator.Send(command));
    }
    
}