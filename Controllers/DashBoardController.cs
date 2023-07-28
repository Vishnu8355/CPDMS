using Microsoft.AspNetCore.Mvc;

namespace CPDMS.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
