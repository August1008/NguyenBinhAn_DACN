using Microsoft.AspNetCore.Mvc;

namespace NguyenBinhAn_DACN.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
