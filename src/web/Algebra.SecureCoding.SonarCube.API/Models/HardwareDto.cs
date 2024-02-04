namespace Algebra.SecureCoding.SonarCube.API.Models
{
    public record HardwareDto
    {
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public string Code { get; set; } = "";
    }
}
