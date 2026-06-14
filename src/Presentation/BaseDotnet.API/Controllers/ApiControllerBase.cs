using Microsoft.AspNetCore.Mvc;
using BaseDotnet.Domain.Common;

namespace BaseDotnet.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        protected ActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
