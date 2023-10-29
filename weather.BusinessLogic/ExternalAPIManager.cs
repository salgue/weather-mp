using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using weather.BusinessLogic.Managers;
using weather.BusinessObjects.Models;

namespace weather.BusinessLogic
{
    public class ExternalAPIManager : IExternalAPIManager
    {
        public async Task<WeatherForecast> GetDataFromExternalAPI(string query, bool addQty, HttpClient client,string apiUrl, string apiKey)
        {
            string url = $"{apiUrl}q={query}&units=imperial&appid={apiKey}";
            if (addQty)
            {
                url += "&cnt=40";
            }
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

               return JsonSerializer.Deserialize<WeatherForecast>(content, options);
            }
            return null;
        }
    }
}
