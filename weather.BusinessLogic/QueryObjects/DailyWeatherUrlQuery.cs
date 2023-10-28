using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace weather.BusinessLogic.QueryObjects
{
    public class DailyWeatherUrlQuery : WeeklyWeatherUrlQuery
    {
        public string SearchDate { get; set; }
    }
}
