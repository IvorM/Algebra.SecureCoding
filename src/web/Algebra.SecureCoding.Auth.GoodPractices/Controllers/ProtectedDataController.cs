using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Algebra.SecureCoding.Auth.GoodPractices.Controllers
{
    [Authorize]
    public class ProtectedDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ElavationRights()
        {
            return View();
        }
    }
}
