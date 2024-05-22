using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Models;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;

namespace Agc.GoodShepherd.Application.Queries.Tukios;

public class GetTukiosQuery : IRequest<ApiResponse<PagedResult<IEnumerable<TukioDm>>>>
{
    public DateTime? From { get; set; } = DateTime.UtcNow.AddMonths(-12);
    public DateTime? To { get; set; } = DateTime.UtcNow;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public bool OrderByDesc { get; set; } = false;
}

public class GetTukiosQueryHandler : IRequestHandler<GetTukiosQuery,ApiResponse<PagedResult<IEnumerable<TukioDm>>>>
{
    private readonly IRepository _repository;
    private readonly ILogger<GetTukiosQueryHandler> _logger;

    public GetTukiosQueryHandler(IRepository repository, ILogger<GetTukiosQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ApiResponse<PagedResult<IEnumerable<TukioDm>>>> Handle(GetTukiosQuery request, CancellationToken cancellationToken)
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
                new("@totalCount", System.Data.SqlDbType.Int)
            };

            param[6].Direction = System.Data.ParameterDirection.Output;

            var tukios = await _repository
                .SQLQuery<TukioDm>(
                    $"EXEC Sp_GetTukios {param[0]},{param[1]},{param[2]},{param[3]},{param[4]},{param[5]}, @totalCount out",
                    param[0], param[1], param[2], param[3], param[4], param[5], param[6]);

            var totalCount = param[^1].Value != null ? Convert.ToInt32(param[^1].Value) : 0;

            var pagedData = new PagedResult<IEnumerable<TukioDm>>
            {
                DataList = tukios,
                TotalCount = totalCount
            };

            return ResponseMessage.Success(pagedData, "SUCCESS");
        }
        catch (Exception e)
        {
            _logger.LogError("Fetching tukios failed with exception {exception}", e);
            return ResponseMessage.Error<PagedResult<IEnumerable<TukioDm>>>(null,
                "Something went wrong, try again.");
        }
    }
}