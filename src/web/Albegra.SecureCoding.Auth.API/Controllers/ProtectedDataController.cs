using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Albegra.SecureCoding.Auth.API.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController()]
    public class ProtectedDataController : ControllerBase
    {
        [HttpGet]
        public List<Object> GetProtectedData()
        {
            return new List<object>()
            {
                new { Name="Ivor Marić", OIB="11111111119" },
                new { Name="Pero Perić", OIB="11111111118" }
            };
        }
    }
}
