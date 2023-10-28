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
            //var response = await GetWeatherFromAPI(searchValue);
            //var results = MapWeatherToViewModel(response);
            var results = new List<WeeklyWeatherVM>()
            {
                new WeeklyWeatherVM() {
                    Date = "10-27-2023",
                    MaxTemperature= 295.82,
                    MinTemperature= 299.38,
                    Icon = "https://openweathermap.org/img/w/01d.png",
                    Description= "clear sky"
                },
                new WeeklyWeatherVM() {
                    Date = "2",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "3",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "4",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "5",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "6",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "7",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "8",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "9",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "10",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "11",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "12",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "13",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "14",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "15",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                },
                new WeeklyWeatherVM() {
                    Date = "16",
                    MaxTemperature= 0,
                    MinTemperature= 0,
                }
            };
            return results;
        }

        private async Task<IEnumerable<IGrouping<DateTime, ForecastItem>>> GetWeeklyWeatherFromAPI(string city)
        {
            IEnumerable<IGrouping<DateTime, ForecastItem>>? forecastGroups = null;
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://api.openweathermap.org/data/2.5/forecast?q={city}&cnt=40&appid=95a040a7cfb018de8fbb8ff1f488e445";

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

            foreach (var dayForecast in forecastGroups)
            {
                DateTime date = dayForecast.Key.Date;
                var maxTemp = dayForecast.Max(f => f.Main.Temp_Max);
                var minTemp = dayForecast.Min(f => f.Main.Temp_Min);
                var icon = dayForecast.Select(f => f.Weather.Select(s => s.Icon));

                var weather = new WeeklyWeatherVM();
                weather.Date = date.ToString("MM/dd/yyyy");
                weather.MaxTemperature = maxTemp - 237.15;
                weather.MinTemperature = minTemp - 237.15;
                weather.Icon = $"https://openweathermap.org/img/w/{icon}.png";

                results.Add(weather);
            }
            return results;
        }

        public async Task<List<DailyWeatherVM>> GetDailyWeather(DailyWeatherUrlQuery query)
        {
            //var response = await GetDailyWeatherFromAPI(query);
            //var results = MapWeatherToDailyViewModel(response);
            var results = new List<DailyWeatherVM>() {
                new DailyWeatherVM()
                {
                    Day = "10/28/2023",
                    HourlyForecast = new List<HourlyWeatherVM>()
                    {
                        new HourlyWeatherVM()
                        {
                            Hour= "02:00",
                            MaxTemperature= 55.45000000000002,
                            MinTemperature= 55.45000000000002
                        },
                         new HourlyWeatherVM()
                        {
                            Hour= "05:00",
                            MaxTemperature= 55.45000000000002,
                            MinTemperature= 55.45000000000002
                        },
                          new HourlyWeatherVM()
                        {
                            Hour= "08:00",
                            MaxTemperature= 55.45000000000002,
                            MinTemperature= 55.45000000000002
                        },
                           new HourlyWeatherVM()
                        {
                            Hour= "011:00",
                            MaxTemperature= 55.45000000000002,
                            MinTemperature= 55.45000000000002
                        },
                    }
                }
            };
            return results;
        }

        private async Task<List<ForecastItem>> GetDailyWeatherFromAPI(DailyWeatherUrlQuery query)
        {
            var dailyForecast = new List<ForecastItem>();
            using (HttpClient client = new HttpClient())
            {
                string url = $"http://api.openweathermap.org/data/2.5/forecast?q={query.SearchValue}&appid=95a040a7cfb018de8fbb8ff1f488e445";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    var forecast = JsonSerializer.Deserialize<WeatherForecast>(content, options);

                    DateTime searchDate = DateTime.Parse(query.SearchDate);

                    dailyForecast = forecast?.List.FindAll(f => DateTimeOffset.FromUnixTimeSeconds(f.Dt).LocalDateTime.Date == searchDate.Date);
                }

                return dailyForecast ?? new List<ForecastItem>();
            }
        }

        private List<DailyWeatherVM> MapWeatherToDailyViewModel(List<ForecastItem> forecastList)
        {
            var results = new List<DailyWeatherVM>();
            var weather = new DailyWeatherVM();

            var date = DateTimeOffset.FromUnixTimeSeconds(forecastList.First().Dt).LocalDateTime;
            weather.Day = date.ToString("MM/dd/yyyy"); ;
            foreach (var dayForecast in forecastList)
            {
                DateTime hour = DateTimeOffset.FromUnixTimeSeconds(dayForecast.Dt).LocalDateTime;

                var hourly = new HourlyWeatherVM();
                hourly.Hour = hour.ToString("HH:mm");
                hourly.MaxTemperature = dayForecast.Main.Temp_Max - 237.15;
                hourly.MinTemperature = dayForecast.Main.Temp_Min - 237.15;

                weather.HourlyForecast.Add(hourly);

            }
            results.Add(weather);
            return results;
        }

    }
}
