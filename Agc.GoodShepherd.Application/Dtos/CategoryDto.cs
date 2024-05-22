using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Application.Dtos;

public static class CategoryDto
{
    public static CategoryDm? ToDto(this Category? x) => x == null
        ? null
        : new CategoryDm()
        {
            Id = x.Id,
            Name = x.Name,
            ImageUrl = x.ImageUrl,
            DateCreated = x.DateCreated,
            DateUpdated = x.DateUpdated
        };
}