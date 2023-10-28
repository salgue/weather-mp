using System.Globalization;
using System.Text.Json;
using weather.BusinessLogic.QueryObjects;
using weather.BusinessObjects.Models;
using weather_mp.Services.Interfaces;
using weather_mp.ViewModels;

namespace weather_mp.Services
{
    public class WeatherForecastService : IWeatherService
    {

        public async Task<List<WeeklyWeatherVM>> GetWeeklyWeather(string searchValue)
        {
            var response = await GetWeeklyWeatherFromAPI(searchValue);
            var results = MapWeatherToWeeklyViewModel(response);
            return results;
        }

        private async Task<IEnumerable<IGrouping<DateTime, ForecastItem>>> GetWeeklyWeatherFromAPI(string city)
        {
            IEnumerable<IGrouping<DateTime, ForecastItem>>? forecastGroups = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&cnt=40&units=imperial&appid=95a040a7cfb018de8fbb8ff1f488e445";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var forecast = JsonSerializer.Deserialize<WeatherForecast>(content, options);

                    DateTime currentDate = DateTime.Now.Date;

                    var dailyForecast = forecast.List.FindAll(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).LocalDateTime.Date >= currentDate);


                    forecastGroups = dailyForecast.GroupBy(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).Date);


                }
                return forecastGroups;
            }
        }

        private List<WeeklyWeatherVM> MapWeatherToWeeklyViewModel(IEnumerable<IGrouping<DateTime, ForecastItem>> forecastGroups)
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
            var response = await GetDailyWeatherFromAPI(query);
            var result = MapWeatherToDailyViewModel(response);
            return result;
        }

        private async Task<List<ForecastItem>> GetDailyWeatherFromAPI(DailyWeatherUrlQuery query)
        {
            var dailyForecast = new List<ForecastItem>();
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://api.openweathermap.org/data/2.5/forecast?q={query.SearchValue}&units=imperial&appid=95a040a7cfb018de8fbb8ff1f488e445";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var forecast = JsonSerializer.Deserialize<WeatherForecast>(content, options);

                    var dateArray = query.SearchDate.Split('/');
                    DateTime searchDate = new DateTime(int.Parse(dateArray[2]), int.Parse(dateArray[0]), int.Parse(dateArray[1]));

                    dailyForecast = forecast?.List.FindAll(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).LocalDateTime.Date == searchDate.Date);
                }

                return dailyForecast ?? new List<ForecastItem>();
            }
        }

        private DailyWeatherVM MapWeatherToDailyViewModel(List<ForecastItem> forecastList)
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
