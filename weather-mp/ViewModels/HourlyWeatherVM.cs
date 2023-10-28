namespace weather_mp.ViewModels
{
    public class HourlyWeatherVM
    {
        public string Hour { get; set; }
        public double Temperature { get; set; }
        public double MaxTemperature { get; set; }
        public double MinTemperature { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
