using Agc.GoodShepherd.Common.Enums;
using Agc.GoodShepherd.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agc.GoodShepherd.Mvc.Controllers.Api
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
