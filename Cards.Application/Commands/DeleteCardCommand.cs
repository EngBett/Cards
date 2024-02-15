using Cards.Application.Interfaces;
using Cards.Common.Enums;
using Cards.Common.Models;
using Cards.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Cards.Application.Commands;

public class DeleteCardCommand:IRequest<ApiResponse<bool>>
{
    public string CardId { get; set; }
}

public class DeleteCardCommandHandler(
    IAppDbContext dbContext,
    ICurrentUserService currentUserService,
    UserManager<ApplicationUser> userManager,
    ILogger<UpdateCardCommandHandler> logger):IRequestHandler<DeleteCardCommand,ApiResponse<bool>>
{
    
    public async Task<ApiResponse<bool>> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(currentUserService.Id);
            if (user == null) return ResponseMessage.Error(false, responseCodes: ResponseCodes.UnAuthorized);

            var card = await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == request.CardId,
                cancellationToken: cancellationToken);

            if (card == null || card.ApplicationUserId != user.Id)
                return ResponseMessage.Error(false, "Card not found", responseCodes: ResponseCodes.NotFound);

            dbContext.Cards.Remove(card);
            var res = await dbContext.SaveChangesAsync(cancellationToken);
        
            return res < 1
                ? ResponseMessage.Error(false, "Something went wrong, try again.")
                : ResponseMessage.Success(true);
        }
        catch (Exception e)
        {
            logger.LogError("Deleting card {cardId} failed with exception {exception}",request.CardId, e);
            return ResponseMessage.Error(false, "Something went wrong, try again.");
        }
    }
}