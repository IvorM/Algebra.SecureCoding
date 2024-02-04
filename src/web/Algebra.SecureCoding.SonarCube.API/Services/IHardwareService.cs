using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;

namespace Algebra.SecureCoding.SonarCube.API.Services
{
    public interface IHardwareService
    {
        Task<List<HardwareDto>?> GetAll();
        Task<HardwareDto?> GetByCode(string code);
        Task<HardwareDto?> Save(NewHardwareDto newHardware);
        Task<HardwareDto?> Update(string code,UpdateHardwareDto updateHardware);
        Task Delete(string code);
    }
}
