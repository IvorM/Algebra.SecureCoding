using Algebra.SecureCoding.Web.Data.SqlInjection;
using Algebra.SecureCoding.Web.Models.SqlInjection;
using Algebra.SecureCoding.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.SecureCoding.Web.Controllers
{
    public class VulnerabilitiesController : Controller
    {
        private readonly IProductService _productService;

        public VulnerabilitiesController(IProductService productService)
        {
            _productService=productService;
        }

        [HttpGet]
        public IActionResult SqlInjection()
        {
            return View(new SqlInjectionSearchRequest());
        }

        [HttpPost]
        public async Task<IActionResult> SqlInjection(SqlInjectionSearchRequest search)
        {
            return View("SqlInjectionResult", await _productService.SearchForProductByName(search.SearchTerm));
        }
    }
}
