using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobAdder.Users.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController:ControllerBase
    {
        [HttpGet("authorized")]
        [Authorize]
        public IActionResult GetAuthorizedMessage()
        {
            return Ok(new
            {
                message = "Authorized Request can get this message"
            });
        }

        [HttpGet("anonymous")]
        [AllowAnonymous]
        public IActionResult GetAnonymousMessage()
        {
            return Ok(new
            {
                message = "Every Request can get this message"
            });
        }
    }
}
