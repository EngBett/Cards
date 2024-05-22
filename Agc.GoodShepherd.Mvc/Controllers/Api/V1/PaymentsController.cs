using Agc.GoodShepherd.Common.RestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers.Api.V1;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/[action]")]
public class PaymentsController:ControllerBase
{
    private readonly IMediator _mediator;

    public PaymentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [AllowAnonymous]
    [HttpPost("{urlToken}")]
    public async Task<IActionResult> C2BCallback([FromBody] MpesaStkPaymentResult command)
    {
        return Ok(command);
    }
}