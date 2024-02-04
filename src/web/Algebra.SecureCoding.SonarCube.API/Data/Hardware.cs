namespace Algebra.SecureCoding.SonarCube.API.Data
{
    public class Hardware
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public HardwareType Type { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
