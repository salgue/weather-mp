using Microsoft.AspNetCore.Mvc;
using weather.BusinessLogic.QueryObjects;
using weather_mp.Services.Interfaces;

namespace weather_mp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        public WeatherController(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        [HttpPost]
        [Route("GetWeeklyWeather")]
        public async Task<IActionResult> GetWeeklyWeather([FromBody] WeeklyWeatherUrlQuery urlQuery)
        {
            var results = await _weatherService.GetWeeklyWeather(urlQuery.SearchValue);
            return Ok(results);
        }

        [HttpPost]
        [Route("GetDailyWeather")]
        public async Task<IActionResult> GetDailyWeather([FromBody] DailyWeatherUrlQuery urlQuery)
        {
            var results = await _weatherService.GetDailyWeather(urlQuery);
            return Ok(results);
        }
    }
}
