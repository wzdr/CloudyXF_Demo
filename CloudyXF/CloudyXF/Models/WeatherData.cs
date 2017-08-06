using System;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using CloudyXF.Models;

namespace CloudyXF.Models
{
    public class WeatherData
    {
        public DateTime Time { get; set; }
        public DateTime TimezoneTime { get; set; }
		public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public Double WindSpeed { get; set; }
        public Double Temperature { get; set; }
        public int Humidity { get; set; }
        public string Icon { get; set; }
        public string Summary { get; set; }
        public List<WeatherDayData> DailyData { get; set; }

        public string TimeZone { get; set; }
        public long UnixTime { get; set; }
        public bool TimezoneIsValid { get; set; }
    }

}
