using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Mapper;
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
            var existingHardware = await _context.Hardwares.SingleOrDefaultAsync(x => x.Code.Equals(code));
            if (existingHardware != null)
            {
                _context.Hardwares.Remove(existingHardware);
                _context.SaveChanges();
            }
        }

        public async Task<List<HardwareDto>?> GetAll()
        {
            return await _context.Hardwares
                .Select(x=>x.ToHardwareDto())
                .ToListAsync();
        }

        public async Task<HardwareDto?> GetByCode(string code)
        {
            var hardware = await _context.Hardwares
                .FirstOrDefaultAsync(x => x.Code.Equals(code));

            if (hardware!=null)
            {
                return hardware.ToHardwareDto();
            }

            return null;
        }

        public async Task<HardwareDto?> Save(NewHardwareDto newHardware)
        {
            _context.Hardwares.Add(newHardware.ToHardware());
            await _context.SaveChangesAsync();

            return newHardware.ToHardwareDto();
        }

        public async Task<HardwareDto?> Update(string code, UpdateHardwareDto updateHardware)
        {
            var existingHardware = await _context.Hardwares.FirstOrDefaultAsync(x => x.Code.Equals(code));
            if (existingHardware != null)
            {
                updateHardware.ToHardware(existingHardware);

                _context.Hardwares.Update(existingHardware);
                await _context.SaveChangesAsync();

                return existingHardware.ToHardwareDto();
            }


            return null;
           
        }
    }
}
