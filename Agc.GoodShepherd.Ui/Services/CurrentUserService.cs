using System.Security.Claims;
using Agc.GoodShepherd.Application.Interfaces;

namespace Agc.GoodShepherd.Ui.Services
{
    public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
    {
        public string Id => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)??"";

        public string UserName => httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Email)??"";

    }
}
