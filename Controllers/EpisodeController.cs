using Microsoft.AspNetCore.Mvc;

namespace SeinfeldAPI.Controllers
{
    public class EpisodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
