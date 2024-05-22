using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Application.Dtos;
using Agc.GoodShepherd.Application.Interfaces;
using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Common.Models;
using Agc.GoodShepherd.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Agc.GoodShepherd.Application.Queries.Projects;

public class GetSingleProjectQuery : IRequest<ApiResponse<ProjectDm>>
{
    public string? ProjectId { get; set; }
}

public class
    GetSingleProjectQueryHandler : IRequestHandler<GetSingleProjectQuery,
    ApiResponse<ProjectDm>>
{
    private readonly IAppDbContext _dbContext;

    public GetSingleProjectQueryHandler(IAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ApiResponse<ProjectDm>> Handle(GetSingleProjectQuery request,
        CancellationToken cancellationToken)
    {
        var project = await _dbContext.Projects
            .Include(x => x.Merchandises)
            .ThenInclude(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken: cancellationToken);

        if (project == null)
            return ResponseMessage.Error<ProjectDm>(null, "Project not found", responseCodes: ResponseCodes.NotFound);

        var categories = project.Merchandises.Select(x => new CategoryDm
        {
            Id = x.Id,
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            Stock = x.Stock
        }).DistinctBy(x=>x.Id);
        var projectDm = project.ToDto();
        projectDm.Categories = categories;

        return ResponseMessage.Success(projectDm);
    }
}