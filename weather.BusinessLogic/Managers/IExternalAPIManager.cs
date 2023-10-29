using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using weather.BusinessObjects;
using weather.BusinessObjects.Models;

namespace weather.BusinessLogic.Managers
{
    public interface IExternalAPIManager : IDependencyScoped
    {
        Task<WeatherForecast> GetDataFromExternalAPI(string query, bool addQty, HttpClient client, string url, string apiKey);
    }
}
