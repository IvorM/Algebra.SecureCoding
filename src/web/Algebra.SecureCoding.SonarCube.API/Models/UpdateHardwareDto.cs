using Algebra.SecureCoding.SonarCube.API.Data;
using System.ComponentModel.DataAnnotations;

namespace Algebra.SecureCoding.SonarCube.API.Models
{
    public class UpdateHardwareDto
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Obavezan podatak")]
        public HardwareType Type { get; set; }
        [Length(7, 7)]
        public string Code { get; set; }
        [Range(0, 10000)]
        public int Stock { get; set; }
        [Range(0, 99999)]
        public decimal Price { get; set; }
    }
}
