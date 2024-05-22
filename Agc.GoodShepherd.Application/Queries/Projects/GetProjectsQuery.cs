using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Dtos;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Models;
using Agc.GoodShepherd.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Application.Queries.Projects;

public class GetProjectsQuery:IRequest<ApiResponse<PagedResult<IEnumerable<ProjectDm>>>>
{
    public DateTime? From { get; set; } = DateTime.UtcNow.AddMonths(-12);
    public DateTime? To { get; set; } = DateTime.UtcNow;
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? SearchTerm { get; set; }
    public ProjectStatuses? Status { get; set; }
    public bool OrderByDesc { get; set; } = false;
}

public class GetProjectsQueryHandler : IRequestHandler<GetProjectsQuery,ApiResponse<PagedResult<IEnumerable<ProjectDm>>>>
{
    private readonly IAppDbContext _dbContext;

    public GetProjectsQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<ApiResponse<PagedResult<IEnumerable<ProjectDm>>>> Handle(GetProjectsQuery request, CancellationToken cancellationToken)
    {
        var projectQuery = _dbContext.Projects
            .Include(x=>x.Merchandises)
            .Where(x =>
            (string.IsNullOrEmpty(request.SearchTerm) || x.Title.Contains(request.SearchTerm) || x.Body.Contains(request.SearchTerm))
            && (request.Status == null || x.Status == request.Status)
            && x.DateCreated >= request.From
            && x.DateCreated <= request.To);

        var total = projectQuery.Count();
        
        var projects = await projectQuery
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => x.ToDto())
            .ToListAsync(cancellationToken);
            
        var pagedResult = new PagedResult<IEnumerable<ProjectDm>>
        {
            DataList = projects,
            TotalCount = total
        };

        return ResponseMessage.Success(pagedResult);

    }
}