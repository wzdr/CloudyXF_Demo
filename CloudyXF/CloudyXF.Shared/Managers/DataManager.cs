using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

using CloudyXF.Models;
using CloudyXF.Services;
using CloudyXF.Helpers;

namespace CloudyXF.Managers
{
    public class DataManager
    {
        public static async Task<WeatherData> GetWeather(double latitude, double longitude)
        {
            var weatherData = new WeatherData();

            try
            {
                string queryString = DarkSkyWeatherService.AuthenticatedBaseURL()
                        + latitude.ToString() + "," + longitude.ToString();

                var response = await GetDataFromService(queryString).ConfigureAwait(false);

                weatherData.Time = Conversions.ConvertFromUnixTimeToLocalTime(response.currently.time);

                weatherData.TimezoneIsValid = false;

                weatherData.Summary = response.currently.summary;
                weatherData.Latitude = response.latitude;
                weatherData.Longitude = response.longitude;
                weatherData.WindSpeed = response.currently.windSpeed;
                weatherData.Temperature = response.currently.temperature;
                weatherData.Humidity = (int)(response.currently.humidity * 100.0);
                weatherData.Icon = response.currently.icon;
                weatherData.TimeZone = response.timezone;
                weatherData.UnixTime = response.currently.time;

                var dailyWeatherForecast = response.daily.data;
                var weatherDayDataList = new List<WeatherDayData>();
                foreach (var item in dailyWeatherForecast)
                {
                    WeatherDayData weatherDayData = new WeatherDayData();
                    weatherDayData.Time = Conversions.ConvertFromUnixTimeToLocalTime(item.time);
                    weatherDayData.Icon = item.icon;
                    weatherDayData.WindSpeed = item.windSpeed;
                    weatherDayData.TemperatureMin = item.temperatureMin;
                    weatherDayData.TemperatureMax = item.temperatureMax;
                    weatherDayData.Humidity = (int)(item.humidity * 100.0);
                    weatherDayDataList.Add(weatherDayData);
                }
                weatherData.DailyData = weatherDayDataList;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get Weather. Exception {0}", ex);
            }

            return weatherData;
        }

        private static async Task<DarkSkyWeatherResponse> GetDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();

            var response = new HttpResponseMessage();
            var weatherData = new DarkSkyWeatherResponse();

            try
            {
                response = await client.GetAsync(queryString);
            }
            catch (WebException e)
            {
                Debug.WriteLine("Register failure " + e.Message);
            }

            if (response != null && response.IsSuccessStatusCode)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                weatherData = JsonConvert.DeserializeObject<DarkSkyWeatherResponse>(json);
            }

            return weatherData;
        }

    }
}
