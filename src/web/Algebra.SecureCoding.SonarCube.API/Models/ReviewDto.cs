using NuGet.Protocol.Plugins;

namespace Algebra.SecureCoding.SonarCube.API.Models
{
    public class ReviewDto
    {
        public string Title { get; set; } = "";
        public string Text { get; set; } = "";
        public int Rating { get; set; }
    }
}
