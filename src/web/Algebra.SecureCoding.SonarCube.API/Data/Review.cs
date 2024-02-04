namespace Algebra.SecureCoding.SonarCube.API.Data
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public int HardwareId { get; set; }
        public Hardware Hardware { get; set; }

    }
}
