using Algebra.SecureCoding.SonarCube.API.Models;
using Algebra.SecureCoding.SonarCube.API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Net;
using System.Runtime.CompilerServices;



namespace Algebra.SecureCoding.SonarCube.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class HardwareController : ControllerBase
    {
        private readonly IHardwareService _hardwareService;

        public HardwareController(IHardwareService hardwareService)
        {
            _hardwareService = hardwareService;
        }

        [HttpGet("GetAll")]
        public async Task<List<HardwareDto>?> GetAll()
        {
            return await _hardwareService.GetAll();
        }


        [HttpGet("{code}",Name = "GetByCode")]
        public async Task<HardwareDto?> GetByCode([FromRoute] string code)
        {
            return await _hardwareService.GetByCode(code);
        }

        [ProducesResponseType(201)]
        [HttpPost("Save")]
        public async Task<HardwareDto?> Save([FromBody] NewHardwareDto hardware)
        {
            return await _hardwareService.Save(hardware);
        }


        [HttpPut("{code}",Name= "Update")]
        public async Task<HardwareDto?> Update([FromRoute]string code, [FromBody] UpdateHardwareDto hardware)
        {
            return await _hardwareService.Update(code, hardware);
        }

 
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [HttpDelete("{code}",Name ="Delete")]
        public async Task Delete([FromRoute]string code)
        {
            await _hardwareService.Delete(code);
        }
    }
}
