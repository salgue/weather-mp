using Microsoft.AspNetCore.Mvc;

namespace weather_mp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
