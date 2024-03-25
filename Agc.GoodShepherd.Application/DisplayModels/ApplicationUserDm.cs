using Agc.GoodShepherd.Domain.Models;

namespace Agc.GoodShepherd.Application.DisplayModels;

public class ApplicationUserDm:BaseDm
{
    public string? UserName { get; set; }
    public string? Email { get; set; }
}

public static class ApplicationUserDto
{
    public static ApplicationUserDm? ToDto(this ApplicationUser? user) => user == null ? null : new ApplicationUserDm
    {
        UserName = user.UserName,
        Email = user.Email,
        DateUpdated = user.DateCreated,
        DateCreated = user.DateCreated
    };
}