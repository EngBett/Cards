using Cards.Application.DisplayModels;
using Cards.Application.Interfaces;
using Cards.Common.Enums;
using Cards.Common.Models;
using Cards.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Cards.Application.Commands;

public class AddCardCommand : IRequest<ApiResponse<CardDm?>>
{
    public string Name { get; set; }
    public string? Color { get; set; }
    public string? Description { get; set; }
}

public class AddCardCommandHandler(
    IAppDbContext dbContext,
    ICurrentUserService currentUserService,
    UserManager<ApplicationUser> userManager,
    ILogger<AddCardCommandHandler> logger)
    : IRequestHandler<AddCardCommand, ApiResponse<CardDm?>>
{
    public async Task<ApiResponse<CardDm?>> Handle(AddCardCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(currentUserService.Id);
            if (user == null) return ResponseMessage.Error<CardDm?>(null, responseCodes: ResponseCodes.UnAuthorized);

            var card = new Card
            {
                ApplicationUserId = user.Id,
                Name = request.Name,
                Color = request.Color,
                Description = request.Description
            };

            dbContext.Cards.Add(card);
            var res = await dbContext.SaveChangesAsync(cancellationToken);

            return res < 1
                ? ResponseMessage.Error<CardDm?>(null, "Something went wrong, try again.")
                : ResponseMessage.Success(card.ToDto());
        }
        catch (Exception e)
        {
            logger.LogError("Adding card failed with exception {exception}", e);
            return ResponseMessage.Error<CardDm?>(null, "Something went wrong, try again.");
        }
    }
}