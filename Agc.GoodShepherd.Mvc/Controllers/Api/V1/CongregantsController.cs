using Agc.GoodShepherd.Application.Commands.Congregants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers.Api.V1;

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