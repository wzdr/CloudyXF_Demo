using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudyXF.Models
{
    public class WeatherForecastData
    {
        public string Day { get; set; }
        public string WeekDate { get; set; }
        public string TemperatureRange { get; set; }
        public string WeekHumidityString { get; set; }
        public string WeekWindSpeed { get; set; }
        public string DisplayIcon { get; set; }
    }
}
