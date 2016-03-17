using System.Web.Mvc;

namespace Asos.FashionEmergency.Web.Controllers
{
    public class MapController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}