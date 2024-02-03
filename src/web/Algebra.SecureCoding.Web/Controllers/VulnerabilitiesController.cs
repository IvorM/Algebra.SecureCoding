using Algebra.SecureCoding.Web.Models.CryptographicFailures;
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

        [HttpGet]
        public IActionResult Cors()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CryptographicFailures()
        {
            return View(new CardInformationDto());
        }

        [HttpPost]
        public IActionResult CryptographicFailures(CardInformationDto cardInformation)
        {
            return View("CryptographicFailuresResult", cardInformation);
        }
    }
}
