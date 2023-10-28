using System.Text.Json;
using weather.BusinessLogic.QueryObjects;
using weather.BusinessObjects.Models;
using weather_mp.Services.Interfaces;
using weather_mp.ViewModels;

namespace weather_mp.Services
{
    public class DashboardService : IDashboardService
    {
        public DashboardService() { }

        public async Task<List<DashboardVM>> GetDashboard(DashboardUrlQuery query)
        {
            var response = await GetDataFromApi(query);
            var results = MapResponseToDashboardViewModel(response);
            return results;
        }

        private async Task<List<WeatherForecastVM>> GetDataFromApi(DashboardUrlQuery query)
        {
            var weathersForecasts = new List<WeatherForecastVM>();
            var forecastGroups = new List<IEnumerable<IGrouping<DateTime, ForecastItem>>>();
            using (HttpClient client = new HttpClient())
            {
                var collection = query.searchData.Split(',');
                foreach (var item in collection)
                {
                    var weathersForecast = new WeatherForecastVM();
                    string url = $"http://api.openweathermap.org/data/2.5/forecast?q={item}&cnt=40&units=imperial&appid=95a040a7cfb018de8fbb8ff1f488e445";

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

                        var dailyForecast = forecast?.List.FindAll(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).LocalDateTime.Date >= currentDate) ?? new List<ForecastItem>();

                        weathersForecast.City = forecast.City.Name;
                        weathersForecast.ForecastGroup = dailyForecast.GroupBy(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).Date);

                        weathersForecasts.Add(weathersForecast);
                    }
                }
                return weathersForecasts;
            }
        }

        private List<DashboardVM> MapResponseToDashboardViewModel(List<WeatherForecastVM> data)
        {
            var results = new List<DashboardVM>();
            if (data != null)
            {
                foreach (var item in data)
                {
                    var dashboardData = new DashboardVM();
                    dashboardData.City = item.City.ToString();

                    foreach (var forecast in item.ForecastGroup)
                    {
                        DateTime date = forecast.Key.Date;
                        var maxTemp = forecast.First().Main.Temp_Max;
                        var minTemp = forecast.First().Main.Temp_Max;
                        var icon = forecast.First().Weather.First().Icon;
                        var description = forecast.First().Weather.First().Description;
                        var speed = forecast.First().Wind.Speed;
                        var temperature = forecast.First().Main.Temp;

                        var weather = new WeeklyWeatherVM();
                        weather.Date = date.ToString("MM/dd/yyyy");
                        weather.MaxTemperature = (maxTemp - 32) / 1.8;
                        weather.MinTemperature = (minTemp - 32) / 1.8;
                        weather.Icon = $"https://openweathermap.org/img/w/{icon}.png";
                        weather.Description = description ?? "";
                        weather.Speed = speed.ToString();
                        weather.Temperature = (temperature - 32) / 1.8; ;

                        dashboardData.Weathers.Add(weather);
                    }

                    results.Add(dashboardData);
                }

            }
            return results;
        }
    }
}
