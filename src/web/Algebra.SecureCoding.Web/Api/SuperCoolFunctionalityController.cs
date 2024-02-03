using Algebra.SecureCoding.Web.Models.SqlInjection;
using Microsoft.AspNetCore.Cors;
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
            var superData = new List<object>()
            {
                new { Advice = "Buy this stock tomorow" },
                new { Advice = "Sell this stock tomorow" }
            };
            return Ok(superData);
        }
    }
}
