using Agc.GoodShepherd.Application.DisplayModels;
using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Application.Dtos;

public static class CongregantDto
{
    public static CongregantDm ToDto(this Congregant? x) => x == null
        ? null
        : new CongregantDm()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            MiddleName = x.MiddleName,
            LastName = x.LastName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
            DateCreated = x.DateCreated,
            DateUpdated = x.DateUpdated
        };
}