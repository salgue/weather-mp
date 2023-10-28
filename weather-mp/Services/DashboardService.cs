using weather_mp.Services.Interfaces;
using weather_mp.ViewModels;

namespace weather_mp.Services
{
    public class DashboardService : IDashboardService
    {
        public DashboardService() { }

        public async Task<List<DashboardVM>> GetDashboard()
        {
            return new List<DashboardVM> 
            { 
                new DashboardVM()
                {
                    City= "Mexicali",
                    Weathers = new List<WeeklyWeatherVM>{ 
                        new WeeklyWeatherVM()
                        {
                            Date = "10-28-2023",
                            MaxTemperature= 25,
                            MinTemperature= 15,
                            Icon = "https://openweathermap.org/img/w/01d.png",
                            Description= "clear sky"                           
                        },   
                        new WeeklyWeatherVM()
                        {
                            Date = "10-29-2023",
                            MaxTemperature= 20,
                            MinTemperature= 15,
                            Icon = "https://openweathermap.org/img/w/01d.png",
                            Description= "clear sky"
                        }
                    }
                },
                new DashboardVM()
                {
                    City= "Londom",
                    Weathers = new List<WeeklyWeatherVM>{
                        new WeeklyWeatherVM()
                        {
                            Date = "10-28-2023",
                            MaxTemperature= 15,
                            MinTemperature= 8,
                            Icon = "https://openweathermap.org/img/w/01d.png",
                            Description= "clear sky"
                        },
                        new WeeklyWeatherVM()
                        {
                            Date = "10-29-2023",
                            MaxTemperature= 20,
                            MinTemperature= 10,
                            Icon = "https://openweathermap.org/img/w/01d.png",
                            Description= "clear sky"
                        }
                    }
                }
            };
        }
    }
}
