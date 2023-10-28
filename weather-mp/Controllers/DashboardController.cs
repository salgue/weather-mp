using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using weather.BusinessLogic.QueryObjects;
using weather_mp.Services.Interfaces;

namespace weather_mp.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService) {
            _dashboardService = dashboardService;
        }

        [HttpPost]
        [Route("GetDashboard")]
        public async Task<IActionResult> GetDashboardData([FromBody] DashboardUrlQuery query)
        {
            var result = await _dashboardService.GetDashboard();
            return Ok(result);
        }
    }
}
