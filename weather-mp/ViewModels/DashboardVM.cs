namespace weather_mp.ViewModels
{
    public class DashboardVM
    {
        public DashboardVM()
        {
            Weathers = new List<WeeklyWeatherVM>();
        }
        public string City { get; set; }
        public List<WeeklyWeatherVM> Weathers { get; set; }
    }
}
