namespace weather_mp.ViewModels
{
    public class WeeklyWeatherVM
    {
        public string Date { get; set; }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Speed { get; set; }
        public double FeelsLike { get; set; }
        public int Humidity { get; set; }
    }
}
