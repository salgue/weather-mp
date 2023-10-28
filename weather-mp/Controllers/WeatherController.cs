using Microsoft.AspNetCore.Mvc;

namespace weather_mp.Controllers
{
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
