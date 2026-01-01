using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace e_CommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("NotFound")]
        public IActionResult NotFoundError()
        {
            return NotFound(); //404
        }

        [HttpGet("BadRequest")]
        public IActionResult BadRequestError()
        {
            return BadRequest(); //400
        }

        [HttpGet("UnAuthorized")]
        public IActionResult UnAuthorizedError()
        {
            return UnAuthorizedError(); //401
        }

        [HttpGet("ServerError")]
        public IActionResult ServerError()
        {
            return Ok("server error"); //500
        }

        [HttpGet("ValidationError")]
        public IActionResult ValidationError()
        {
            ModelState.AddModelError("validation error 1", "validation error details");
            ModelState.AddModelError("validation error 2", "validation error details");
            return ValidationProblem();
        }
    }
}
