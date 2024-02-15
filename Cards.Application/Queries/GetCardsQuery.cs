using Cards.Application.Commands;
using Cards.Application.DisplayModels;
using Cards.Application.Interfaces;
using Cards.Common.Enums;
using Cards.Common.Models;
using Cards.Domain.Enums;
using Cards.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Cards.Application.Queries;

public class GetCardsQuery : IRequest<ApiResponse<PagedResult<IEnumerable<CardDm>>?>>
{
    public DateTime? From { get; set; } = DateTime.UtcNow.AddMonths(-12);
    public DateTime? To { get; set; } = DateTime.UtcNow;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public CardStatuses? Status { get; set; }
    public CardsSortBy? SortBy { get; set; } = CardsSortBy.DateCreated;
    public bool OrderByDesc { get; set; } = false;
}

public class GetCardsQueryHandler(
    IAppDbContext dbContext,
    ICurrentUserService currentUserService,
    UserManager<ApplicationUser> userManager,
    IRepository repository,
    ILogger<UpdateCardCommandHandler> logger)
    : IRequestHandler<GetCardsQuery, ApiResponse<PagedResult<IEnumerable<CardDm>>?>>
{
    public async Task<ApiResponse<PagedResult<IEnumerable<CardDm>>?>> Handle(GetCardsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await userManager.FindByIdAsync(currentUserService.Id);
            if (user == null) return ResponseMessage.Error<PagedResult<IEnumerable<CardDm>>?>(null, responseCodes: ResponseCodes.UnAuthorized);

            var applicationUserId = string.Empty;

            if (!await userManager.IsInRoleAsync(user, nameof(Roles.Admin)))
            {
                applicationUserId = user.Id;
            }
            
            var param = new SqlParameter[]
            {
                new("@pageindex", request.Page), 
                new("@pagesize", request.PageSize), 
                new("@startDate", request.From),
                new("@endDate", request.To),
                new("@cardStatus", request.Status.HasValue ? $"{request.Status.Value}" : ""),
                new("@sortBy", request.SortBy.HasValue ? request.SortBy.Value.ToString() : ""),
                new("@orderBy", request.OrderByDesc ? "DESC" : "ASC"),
                new("@keyword", !string.IsNullOrEmpty(request.SearchTerm) ? request.SearchTerm : ""),
                new("@applicationUserId",!string.IsNullOrEmpty(applicationUserId) ? applicationUserId : ""),
                new("@totalCount", System.Data.SqlDbType.Int)
            };

            param[9].Direction = System.Data.ParameterDirection.Output;

            var cards = await repository
                .SQLQuery<CardDm>(
                    $"EXEC Sp_GetTransactions {param[0]},{param[1]},{param[2]},{param[3]},{param[4]},{param[5]},{param[6]},{param[7]},{param[8]}, @totalCount out",
                    param[0], param[1], param[2], param[3], param[4], param[5], param[6], param[7], param[8], param[9]);

            var totalCount = param[^1].Value != null ? Convert.ToInt32(param[^1].Value) : 0;

            var pagedData = new PagedResult<IEnumerable<CardDm>>
            {
                DataList = cards,
                TotalCount = totalCount
            };

            return ResponseMessage.Success<PagedResult<IEnumerable<CardDm>>?>(pagedData, "SUCCESS");
        }
        catch (Exception e)
        {
            logger.LogError("Fetching cards failed with exception {exception}", e);
            return ResponseMessage.Error<PagedResult<IEnumerable<CardDm>>?>(null, "Something went wrong, try again.");
        }
        
    }
}