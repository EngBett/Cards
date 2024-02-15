using Cards.Application.Commands;
using Cards.Application.Queries;
using Cards.Common.Enums;
using Cards.Common.Extensions;
using Cards.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using StackExchange.Redis;

namespace Cards.Api.Controllers.V1;

[ApiController]
[Route("api/v1/[controller]")]
public class CardsController : BaseController
{
    private readonly IMediator _mediator;

    public CardsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] AddCardCommand command)
    {
        return CustomResponse(await _mediator.Send(command));
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> All([FromQuery] GetCardsQuery query)
    {
        return CustomResponse(await _mediator.Send(query));
    }

    [HttpGet("{cardId}")]
    public async Task<IActionResult> Get(string cardId)
    {
        return CustomResponse(await _mediator.Send(new GetSingleCardQuery
        {
            CardId = cardId
        }));
    }

    [HttpPut("{cardId}")]
    public async Task<IActionResult> Update([FromBody] UpdateCardCommand command, string cardId)
    {
        command.CardId = cardId;
        return CustomResponse(await _mediator.Send(command));
    }
    
    [HttpDelete("{cardId}")]
    public async Task<IActionResult> Delete(string cardId)
    {
        return CustomResponse(await _mediator.Send(new DeleteCardCommand{CardId = cardId}));
    }

    [AllowAnonymous]
    [HttpGet("enums")]
    public IActionResult CardEnums()
    {
        var cardStatuses = EnumUtilExtension.GetEnumeratedValues<CardStatuses>();
        var cardSortByValues = EnumUtilExtension.GetEnumeratedValues<CardsSortBy>();
        return Ok(new
        {
            cardStatuses, cardSortByValues
        });
    }
}