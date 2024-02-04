using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Algebra.SecureCoding.SonarCube.API.Services
{
    public class HardwareServices : IHardwareService
    {
        private readonly DataContext _context;
        public HardwareServices(DataContext context)
        {
            _context = context;
        }
        public async Task Delete(string code)
        {
            _context.Hardwares.Remove(await _context.Hardwares.SingleOrDefaultAsync(x => x.Code.Equals(code)));
            _context.SaveChanges();
        }

        public async Task<List<HardwareDto>?> GetAll()
        {
            return await _context.Hardwares
                .Select(x => new HardwareDto
                {
                    Code = x.Code,
                    Name = x.Name,
                    Price=x.Price
                })
                .ToListAsync();
        }

        public async Task<HardwareDto?> GetByCode(string code)
        {
            return await _context.Hardwares
                .Select(x=> new HardwareDto
                {
                    Code= x.Code,
                    Name= x.Name,
                    Price= x.Price
                })
                .FirstOrDefaultAsync(x => x.Code.Equals(code));
        }

        public async Task<HardwareDto?> Save(NewHardwareDto hardware)
        {
            _context.Hardwares.Add(new Hardware
            {
                Code=hardware.Code,
                Name=hardware.Name,
                Price=hardware.Price,
                Stock=hardware.Stock,
                Type=hardware.Type
            });
            await _context.SaveChangesAsync();

            return new HardwareDto { Code = hardware.Code, Name = hardware.Name, Price = hardware.Price };
        }

        public async Task<HardwareDto?> Update(string code, UpdateHardwareDto hardware)
        {
            var existingHardware = await _context.Hardwares.FirstOrDefaultAsync(x => x.Code.Equals(code));
            existingHardware.Price=hardware.Price;
            existingHardware.Name=hardware.Name;
            existingHardware.Stock=hardware.Stock;
            existingHardware.Type=hardware.Type;

            _context.Hardwares.Update(existingHardware);
            await _context.SaveChangesAsync();

            return new HardwareDto
            {
                Code = code,
                Name = existingHardware.Name,
                Price = existingHardware.Price
            };
        }
    }
}
