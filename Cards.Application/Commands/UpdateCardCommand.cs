using System.Text.Json.Serialization;
using Cards.Application.DisplayModels;
using Cards.Application.Interfaces;
using Cards.Common.Enums;
using Cards.Common.Models;
using Cards.Domain.Enums;
using Cards.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cards.Application.Commands;

public class UpdateCardCommand : IRequest<ApiResponse<CardDm?>>
{
    [JsonIgnore]
    public string CardId { get; set; }
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
    public CardStatuses Status { get; set; }
}

public class UpdateCardCommandHandler(
    IAppDbContext dbContext,
    ICurrentUserService currentUserService,
    UserManager<ApplicationUser> userManager,
    ILogger<UpdateCardCommandHandler> logger) : IRequestHandler<UpdateCardCommand, ApiResponse<CardDm?>>
{
    public async Task<ApiResponse<CardDm?>> Handle(UpdateCardCommand request, CancellationToken cancellationToken)
    {

        try
        {
            var user = await userManager.FindByIdAsync(currentUserService.Id);
            if (user == null) return ResponseMessage.Error<CardDm?>(null, responseCodes: ResponseCodes.UnAuthorized);

            var card = await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId,
                cancellationToken: cancellationToken);

            if (card == null || card.ApplicationUserId != user.Id)
                return ResponseMessage.Error<CardDm?>(null, "Card not found", responseCodes: ResponseCodes.NotFound);

            card.Name = request.Name;
            card.Color = request.Color;
            card.Status = request.Status;
            card.Description = request.Description;
            card.DateUpdated = DateTime.UtcNow;

            var res = await dbContext.SaveChangesAsync(cancellationToken);
        
            return res < 1
                ? ResponseMessage.Error<CardDm?>(null, "Something went wrong, try again.")
                : ResponseMessage.Success<CardDm?>(card.ToDto());
        }
        catch (Exception e)
        {
            logger.LogError("Updating card {cardId} failed with exception {exception}",request.CardId, e);
            return ResponseMessage.Error<CardDm?>(null, "Something went wrong, try again.");
        }

    }
}