using weather.BusinessObjects;
using weather.BusinessObjects.Models;
using weather_mp.ViewModels;

namespace weather_mp.Services.Interfaces
{
    public interface IDashboardService : IDependencyScoped
    {
        Task<List<DashboardVM>> GetDashboard();
    }
}
