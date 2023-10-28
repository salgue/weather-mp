using weather.BusinessLogic.QueryObjects;
using weather.BusinessObjects;
using weather_mp.ViewModels;

namespace weather_mp.Services.Interfaces
{
    public interface IWeatherService : IDependencyScoped
    {
        Task<List<WeeklyWeatherVM>> GetWeeklyWeather(string searchValue);
        Task<DailyWeatherVM> GetDailyWeather(DailyWeatherUrlQuery query);
    }
}
