using Microsoft.AspNetCore.Mvc;

namespace VehicleInventory.WebAPI.Controllers
{
    public class VehicleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
