using Algebra.SecureCoding.Web.Models.Shared;
using Algebra.SecureCoding.Web.Models.SqlInjection;

namespace Algebra.SecureCoding.Web.Services
{
    public interface IProductService
    {
        Task<Result<List<ProductDto>>> SearchForProductByName(string name);
    }
}
