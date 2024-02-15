using Cards.Application.Commands;
using Cards.Application.DisplayModels;
using Cards.Application.Interfaces;
using Cards.Common.Enums;
using Cards.Common.Models;
using Cards.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cards.Application.Queries;

public class GetSingleCardQuery:IRequest<ApiResponse<CardDm?>>
{
    public string CardId { get; set; }
}

public class GetSingleCardQueryHandler(
    IAppDbContext dbContext,
    ICurrentUserService currentUserService,
    UserManager<ApplicationUser> userManager,
    ILogger<UpdateCardCommandHandler> logger) : IRequestHandler<GetSingleCardQuery,ApiResponse<CardDm?>>
{
    
    public async Task<ApiResponse<CardDm?>> Handle(GetSingleCardQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(currentUserService.Id);
            if (user == null) return ResponseMessage.Error<CardDm?>(null, responseCodes: ResponseCodes.UnAuthorized);

            var card = await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId,
                cancellationToken: cancellationToken);

            if (card == null || (card.ApplicationUserId != user.Id && !await userManager.IsInRoleAsync(user,nameof(Roles.Admin))))
                return ResponseMessage.Error<CardDm?>(null, "Card not found", responseCodes: ResponseCodes.NotFound);
            
            return ResponseMessage.Success(card.ToDto());
        }
        catch (Exception e)
        {
            logger.LogError("Fetching card {cardId} failed with exception {exception}",request.CardId, e);
            return ResponseMessage.Error<CardDm?>(null, "Something went wrong, try again.");
        }
    }
}