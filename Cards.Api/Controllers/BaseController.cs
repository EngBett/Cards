using Cards.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cards.Common.Models;

namespace Cards.Api.Controllers
{
    [Authorize]
    public abstract class BaseController : ControllerBase
    {

        protected IActionResult CustomResponse<T>(ApiResponse<T> result)
        {
            return result.Code switch
            {
                ResponseCodes.Fail => BadRequest(result),
                ResponseCodes.ValidationError => BadRequest(result),
                ResponseCodes.NotFound => NotFound(result),
                ResponseCodes.UnAuthorized => Unauthorized(result),
                ResponseCodes.Forbidden => Forbid(),
                _ => Ok(result)
            };
        }

    }
}
