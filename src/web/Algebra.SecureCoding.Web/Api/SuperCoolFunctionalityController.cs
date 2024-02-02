using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.SecureCoding.Web.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperCoolFunctionalityController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSuperCoolFunctionality()
        {
            return Ok(new { superData = "Super cool functionality" });
        }
    }
}
