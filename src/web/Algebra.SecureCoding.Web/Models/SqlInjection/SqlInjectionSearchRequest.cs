using System.ComponentModel.DataAnnotations;

namespace Algebra.SecureCoding.Web.Models.SqlInjection
{
    public class SqlInjectionSearchRequest
    {
        [Required(ErrorMessage ="Search term is required")]
        public string SearchTerm { get; set; }
    }
}
