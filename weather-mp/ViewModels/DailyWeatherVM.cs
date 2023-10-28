namespace weather_mp.ViewModels
{
    public class DailyWeatherVM
    {
        public DailyWeatherVM()
        {
            HourlyForecast = new List<HourlyWeatherVM>();
        }
        public string Day { get; set; }
        public List<HourlyWeatherVM> HourlyForecast { get; set; }
    }
}
