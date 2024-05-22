using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Application.Dtos;

public static class ProjectDto
{
    public static ProjectDm ToDto(this Project? x) => x == null
        ? null
        : new ProjectDm()
        {
            Id = x.Id,
            Body = x.Body,
            Title = x.Title,
            Subtitle = x.Subtitle,
            TargetAmount = x.TargetAmount,
            CurrentAmount = x.CurrentAmount,
            DateCreated = x.DateCreated,
            DateUpdated = x.DateUpdated
        };
}