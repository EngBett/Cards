using Cards.Application.Interfaces;
using System.Security.Claims;
namespace Cards.Api.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public string Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)??"";

        public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)??"";

    }
}
