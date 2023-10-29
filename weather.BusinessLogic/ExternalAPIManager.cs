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
        public async Task<WeatherForecast> GetDataFromExternalAPI(string query, bool addQty, HttpClient client)
        {
            string url = $"http://api.openweathermap.org/data/2.5/forecast?q={query}&units=imperial&appid=95a040a7cfb018de8fbb8ff1f488e445";
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
