using System;
using System.Threading.Tasks;

using CloudyXF.Models;
using CloudyXF.Services;
using CloudyXF.Helpers;
using CloudyXF.Managers;


namespace CloudyXF.ViewModels
{
    public class WeatherViewController
    {
        DarkSkyWeatherService weatherService { get; } = new DarkSkyWeatherService();

        bool _isBusy = false;

        public async Task<WeatherData> ExecuteGetWeatherCommand()
        {
            var weatherData = new WeatherData();
            if (!_isBusy)
            {
                _isBusy = true;
                weatherData = await DataManager.GetWeather(UserSettings.DefaultLatitude, UserSettings.DefaultLongitude);
                _isBusy = false;
            }

            return weatherData;
        }

    }
}
