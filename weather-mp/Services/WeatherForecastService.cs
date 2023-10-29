using System.Globalization;
using System.Text.Json;
using weather.BusinessLogic.Managers;
using weather.BusinessLogic.QueryObjects;
using weather.BusinessObjects.Models;
using weather_mp.Services.Interfaces;
using weather_mp.ViewModels;

namespace weather_mp.Services
{
    public class WeatherForecastService : IWeatherService
    {
        private readonly IExternalAPIManager _externalAPIManager;

        public WeatherForecastService(IExternalAPIManager externalAPIManager) { 
            _externalAPIManager = externalAPIManager;
        }
    public async Task<List<WeeklyWeatherVM>> GetWeeklyWeather(string searchValue)
        {
            var response = await BuildWeeklyWeatherData(searchValue);
            var results = MapDataToWeeklyViewModel(response);
            return results;
        }

        private async Task<IEnumerable<IGrouping<DateTime, ForecastItem>>> BuildWeeklyWeatherData(string city)
        {
            IEnumerable<IGrouping<DateTime, ForecastItem>>? forecastGroups = null;
            using (HttpClient client = new HttpClient())
            {
                var forecast = await _externalAPIManager.GetDataFromExternalAPI(city, true, client);

                DateTime currentDate = DateTime.Now.Date;

                var dailyForecast = forecast.List.FindAll(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).LocalDateTime.Date >= currentDate);


                forecastGroups = dailyForecast.GroupBy(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).Date);

                return forecastGroups;
            }
        }

        private List<WeeklyWeatherVM> MapDataToWeeklyViewModel(IEnumerable<IGrouping<DateTime, ForecastItem>> forecastGroups)
        {
            var results = new List<WeeklyWeatherVM>();
            if (forecastGroups != null)
            {
                foreach (var dayForecast in forecastGroups)
                {
                    DateTime date = dayForecast.Key.Date;
                    var maxTemp = dayForecast.Max(f => f.Main.Temp_Max);
                    var minTemp = dayForecast.Min(f => f.Main.Temp_Min);
                    var icon = dayForecast.Max(f => f.Weather.Max(x => x.Icon));
                    var description = dayForecast.Max(f => f.Weather.Max(x => x.Description));
                    var speed = dayForecast.Max(f => f.Wind.Speed);
                    var temperature = dayForecast.Max(f => f.Main.Temp);

                    var weather = new WeeklyWeatherVM();
                    weather.Date = date.ToString("MM/dd/yyyy");
                    weather.MaxTemperature = (maxTemp - 32) / 1.8;
                    weather.MinTemperature = (minTemp - 32) / 1.8;
                    weather.Icon = $"https://openweathermap.org/img/w/{icon}.png";
                    weather.Description = description ?? "";
                    weather.Speed = speed.ToString();
                    weather.Temperature = (temperature - 32) / 1.8;

                    results.Add(weather);
                }
            }
            return results;
        }

        public async Task<DailyWeatherVM> GetDailyWeather(DailyWeatherUrlQuery query)
        {
            var response = await BuildDailyWeatherData(query);
            var result = MapDataToDailyViewModel(response);
            return result;
        }

        private async Task<List<ForecastItem>> BuildDailyWeatherData(DailyWeatherUrlQuery query)
        {
            var dailyForecast = new List<ForecastItem>();
            using (HttpClient client = new HttpClient())
            {
                var forecast = await _externalAPIManager.GetDataFromExternalAPI(query.SearchValue, false, client);

                var dateArray = query.SearchDate.Split('/');
                DateTime searchDate = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[0]), int.Parse(dateArray[1]));

                dailyForecast = forecast?.List.FindAll(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).LocalDateTime.Date == searchDate.Date);

                return dailyForecast ?? new List<ForecastItem>();
            }
        }

        private DailyWeatherVM MapDataToDailyViewModel(List<ForecastItem> forecastList)
        {
            if (forecastList.Count == 0) return null;

            var weather = new DailyWeatherVM();
            var date = DateTimeOffset.FromUnixTimeSeconds(forecastList.First().Dt).LocalDateTime;
            weather.Day = date.ToString("MM/dd/yyyy");

            foreach (var dayForecast in forecastList)
            {
                DateTime hour = DateTimeOffset.FromUnixTimeSeconds(dayForecast.Dt).LocalDateTime;

                var hourly = new HourlyWeatherVM();
                hourly.Hour = hour.ToString("HH:mm");
                hourly.MaxTemperature = (dayForecast.Main.Temp_Max - 32) / 1.8;
                hourly.MinTemperature = (dayForecast.Main.Temp_Min - 32) / 1.8;
                hourly.Description = dayForecast.Weather.First().Description;
                var icon = dayForecast.Weather.First().Icon;
                hourly.Icon = $"https://openweathermap.org/img/w/{icon}.png";
                hourly.Temperature = (dayForecast.Main.Temp - 32) / 1.8;

                weather.HourlyForecast.Add(hourly);
            }

            return weather;
        }

    }
}
