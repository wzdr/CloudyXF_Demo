using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CloudyXF.Models
{
    public class WeatherDayData
    {
        public DateTime Time { get; set; }
        public string Icon { get; set; }
        public Double WindSpeed { get; set; }
        public Double TemperatureMin { get; set; }
        public Double TemperatureMax { get; set; }
        public int Humidity { get; set; }
    }
}