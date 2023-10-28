using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather.BusinessObjects.Models
{
    public class ForecastItem
    {
        public long Dt { get; set; }
        public MainData Main { get; set; }
        public List<WeatherInformation> Weather { get; set; }
        public WindInformation Wind { get; set; }
    }
}
