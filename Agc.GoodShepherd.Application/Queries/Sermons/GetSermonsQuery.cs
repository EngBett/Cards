using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Agc.GoodShepherd.Application.Queries.Sermons;

public class GetSermonsQuery : IRequest<ApiResponse<PagedResult<IEnumerable<SermonDm>>>>
{
    public DateTime? From { get; set; } = DateTime.UtcNow.AddMonths(-12);
    public DateTime? To { get; set; } = DateTime.UtcNow;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public string? SermonId { get; set; }
    public bool OrderByDesc { get; set; } = false;
}

public class
    GetSermonsQueryHandler : IRequestHandler<GetSermonsQuery,
        ApiResponse<PagedResult<IEnumerable<SermonDm>>>>
{
    private readonly ILogger<GetSermonsQueryHandler> _logger;
    private readonly IRepository _repository;

    public GetSermonsQueryHandler(ILogger<GetSermonsQueryHandler> logger, IRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<ApiResponse<PagedResult<IEnumerable<SermonDm>>>> Handle(GetSermonsQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            var param = new SqlParameter[]
            {
                new("@pageindex", request.Page),
                new("@pagesize", request.PageSize),
                new("@startDate", request.From),
                new("@endDate", request.To),
                new("@orderBy", request.OrderByDesc ? "DESC" : "ASC"),
                new("@keyword", !string.IsNullOrEmpty(request.SearchTerm) ? request.SearchTerm : ""),
                new("@sermonId",!string.IsNullOrEmpty(request.SermonId) ? request.SermonId : ""),
                new("@totalCount", System.Data.SqlDbType.Int)
            };

            param[^1].Direction = System.Data.ParameterDirection.Output;

            var sermons = await _repository
                .SQLQuery<SermonDm>(
                    $"EXEC Sp_GetSermons {param[0]},{param[1]},{param[2]},{param[3]},{param[4]},{param[5]},{param[6]}, @totalCount out",
                    param[0], param[1], param[2], param[3], param[4], param[5], param[6], param[7]);

            var totalCount = param[^1].Value != null ? Convert.ToInt32(param[^1].Value) : 0;

            var pagedData = new PagedResult<IEnumerable<SermonDm>>
            {
                DataList = sermons,
                TotalCount = totalCount
            };

            return ResponseMessage.Success(pagedData, "SUCCESS");
        }
        catch (Exception e)
        {
            _logger.LogError("Fetching sermons failed with exception {exception}", e);
            return ResponseMessage.Error<PagedResult<IEnumerable<SermonDm>>>(null,
                "Something went wrong, try again.");
        }
    }
}