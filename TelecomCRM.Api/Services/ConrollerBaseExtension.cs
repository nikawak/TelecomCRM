using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Application.ResponseModels;

namespace TelecomCRM.Api.Services
{
    public class TelecomBaseController : ControllerBase
    {
        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            return StatusCode(result.ErrorName.StatusCode, new { result.ErrorName.Code, result.ErrorName.Description });
        }
    }
}
