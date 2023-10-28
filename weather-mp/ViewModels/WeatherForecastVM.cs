using weather.BusinessObjects.Models;

namespace weather_mp.ViewModels
{
    public class WeatherForecastVM
    {
        public IEnumerable<IGrouping<DateTime, ForecastItem>> ForecastGroup { get; set; }
        public string City { get; set; }
    }
}
