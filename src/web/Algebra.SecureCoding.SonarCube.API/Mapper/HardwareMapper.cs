using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;

namespace Algebra.SecureCoding.SonarCube.API.Mapper
{
    public static class HardwareMapper
    {
        public static HardwareDto ToHardwareDto(this Hardware hardware)
        {
            return new HardwareDto
            {
                Code = hardware.Code,
                Name = hardware.Name,
                Price = hardware.Price
            };
        }

        public static HardwareDto ToHardwareDto(this NewHardwareDto hardware)
        {
            return new HardwareDto
            {
                Code = hardware.Code,
                Name = hardware.Name,
                Price = hardware.Price
            };
        }

        public static void ToHardware(this UpdateHardwareDto updateHardwareDto,Hardware hardware)
        {
            hardware.Price = updateHardwareDto.Price;
            hardware.Name = updateHardwareDto.Name;
            hardware.Stock = updateHardwareDto.Stock;
            hardware.Type = updateHardwareDto.Type;
            hardware.Code = updateHardwareDto.Code;
        }

        public static Hardware ToHardware(this NewHardwareDto newHardware)
        {
            return new Hardware
            {
                Code = newHardware.Code,
                Name = newHardware.Name,
                Price = newHardware.Price,
                Stock = newHardware.Stock,
                Type = newHardware.Type
            };
        }
    }
}
