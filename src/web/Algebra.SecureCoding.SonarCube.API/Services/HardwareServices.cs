using Algebra.SecureCoding.SonarCube.API.Data;
using Algebra.SecureCoding.SonarCube.API.Models;
using Microsoft.Data.SqlClient;
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
            var hardwareToDelete = await _context.Hardwares.SingleOrDefaultAsync(x => x.Code.Equals(code));
            _context.Hardwares.Remove(hardwareToDelete);
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

        public async Task<HardwareDto> GetByCode(string code)
        {
            var hardware= await _context.Hardwares.FromSqlRaw("SELECT * FROM Hardwares WHERE Code = '" + code + "'")
                .FirstOrDefaultAsync();

            return new HardwareDto
            {
                Code = hardware.Code,
                Name = hardware.Name,
                Price = hardware.Price
            };
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

        public async Task<HardwareDto> Update(string code, UpdateHardwareDto hardware)
        {
            //Zašto ovo ne detektira otvoreni connection???
            if (code==hardware.Code)
            {
                var _connectionString = "Server=(localdb)\\mssqllocaldb;Database=SecureCodingStore;Trusted_Connection=True;MultipleActiveResultSets=true";
                var connection = new SqlConnection(_connectionString);
                connection.Open();
                var command = new SqlCommand("UPDATE Hardwares SET Name = @Name, Price = @Price, Stock = @Stock, Type = @Type WHERE Code = @Code", connection);
                command.Parameters.AddWithValue("@Name", hardware.Name);
                command.Parameters.AddWithValue("@Price", hardware.Price);
                command.Parameters.AddWithValue("@Stock", hardware.Stock);
                command.Parameters.AddWithValue("@Type", hardware.Type);
                command.Parameters.AddWithValue("@Code", code);

                command.ExecuteNonQuery();
                return new HardwareDto
                {
                    Code = code,
                    Name = hardware.Name,
                    Price = hardware.Price
                };
            }
            else
            {
                return null;
            }
            
        }

    }
}
