using Microsoft.AspNetCore.Mvc;

namespace NguyenBinhAn_DACN.Areas.Teachera.Controllers
{
    [Area("Teachera")]
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
