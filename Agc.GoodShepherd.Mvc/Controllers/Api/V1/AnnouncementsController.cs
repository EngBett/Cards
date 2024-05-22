using Agc.GoodShepherd.Application.Queries.Announcements;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers.Api.V1;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/[action]")]
public class AnnouncementsController : BaseController
{
    private readonly IMediator _mediator;

    public AnnouncementsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> All([FromQuery] GetAnnouncementsQuery query)
    {
        return CustomResponse(await _mediator.Send(query));
    }
    
}