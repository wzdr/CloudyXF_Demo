using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MvvmHelpers;

using CloudyXF.Models;
using CloudyXF.Services;
using CloudyXF.Helpers;
using CloudyXF.Managers;
using System.Globalization;

namespace CloudyXF.ViewModels
{
    public class WeatherPageViewModel : BaseViewModel
    {
        private DateTime timezoneDateTime;
        private bool timezoneIsValid;

        string dateString = string.Empty;
        public string Date
        {
            get { return dateString; }
            set { SetProperty(ref dateString, value); }
        }

        string timeString = string.Empty;
        public string TimeString
        {
            get { return timeString; }
            set { SetProperty(ref timeString, value); }
        }

        string timezoneInfoString = string.Empty;
        public string TimezoneInfo
        {
            get { return timezoneInfoString; }
            set { SetProperty(ref timezoneInfoString, value); }
        }

        string timezoneTimeString = string.Empty;
        public string TimezoneTime
        {
            get { return timezoneTimeString; }
            set { SetProperty(ref timezoneTimeString, value); }
        }

        string timezoneString = string.Empty;
        public string Timezone
        {
            get { return timezoneString; }
            set { SetProperty(ref timezoneString, value); }
        }

        bool isTimezoneValid;
        public bool TimezoneIsValid
        {
            get { return isTimezoneValid; }
            set { SetProperty(ref isTimezoneValid, value); }
        }

        string summaryString = string.Empty;
        public string Summary
        {
            get { return summaryString; }
            set { SetProperty(ref summaryString, value); }
        }

        string temperatureString = string.Empty;
        public string Temperature
        {
            get { return temperatureString; }
            set { SetProperty(ref temperatureString, value); }
        }

        string humidityString = string.Empty;
        public string Humidity
        {
            get { return humidityString; }
            set { SetProperty(ref humidityString, value); }
        }

        string windSpeedString = string.Empty;
        public string WindSpeed
        {
            get { return windSpeedString; }
            set { SetProperty(ref windSpeedString, value); }
        }

        string currentLocationString = string.Empty;
        public string CurrentLocation
        {
            get { return currentLocationString; }
            set { SetProperty(ref currentLocationString, value); }
        }

        bool localTimeOn;
        public bool LocalTimeOn
        {
            get { return localTimeOn; }
            set { SetProperty(ref localTimeOn, value); }
        }

        ObservableRangeCollection<WeatherForecastData> weekWeatherData;
        public ObservableRangeCollection<WeatherForecastData> WeekWeatherData
        {
            get { return weekWeatherData; }
            set { weekWeatherData = value; OnPropertyChanged(); }
        }

        string dayString = string.Empty;
        public string Day
        {
            get { return dayString; }
            set { SetProperty(ref dayString, value); }
        }

        string weekDateString = string.Empty;
        public string WeekDate
        {
            get { return weekDateString; }
            set { SetProperty(ref weekDateString, value); }
        }

        string temperatureRange = string.Empty;
        public string TemperatureRange
        {
            get { return temperatureRange; }
            set { SetProperty(ref temperatureRange, value); }
        }

        string weekHumidityString = string.Empty;
        public string WeekHumidityString
        {
            get { return weekHumidityString; }
            set { SetProperty(ref weekHumidityString, value); }
        }

        string weekWindSpeedString = string.Empty;
        public string WeekWindSpeed
        {
            get { return weekWindSpeedString; }
            set { SetProperty(ref weekWindSpeedString, value); }
        }

        string dayImageString = string.Empty;
        public string DayImageString
        {
            get { return dayImageString; }
            set { SetProperty(ref dayImageString, value); }
        }

        public ObservableRangeCollection<WeatherForecastData> UpdateUi(WeatherData weatherData)
        {
            UpdateDayWeatherData(weatherData);
            return UpdateDailyForecast(weatherData.DailyData);
        }

        private void UpdateDayWeatherData(WeatherData weatherData)
        {
            //DayWeatherData = weatherData;

            var dateTime = weatherData.Time;
            timezoneDateTime = weatherData.TimezoneTime;
            timezoneIsValid = weatherData.TimezoneIsValid;

            var timeZone = weatherData.TimeZone;
            var unixTime = weatherData.UnixTime;

            var weekDay = dateTime.DayOfWeek;
            var month = dateTime.Month;
            var day = dateTime.Day;
            var summary = weatherData.Summary;
            var temperature = weatherData.Temperature;
            var humidity = weatherData.Humidity;
            var windSpeed = weatherData.WindSpeed;
            var icon = weatherData.Icon;

            var weekDayName = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)weekDay];

            //update properties
            DayImageString = GetImageNameForIcon(icon, big: true);
            Date = weekDayName
                + ", " + day.ToString() + " "
                + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[(int)month - 1];

            TimeString = GetTimeString(dateTime);
            TimezoneTime = GetTimezoneTime();
            Timezone = timeZone;
            TimezoneIsValid = timezoneIsValid;
            Summary = summary;
            Humidity = String.Format("{0} %", humidity);
            Temperature = GetTemperature(temperature);
            WindSpeed = GetWindSpeed(windSpeed);

            CurrentLocation = UserSettings.SelectedLocation;
            LocalTimeOn = UserSettings.LocalTimeOnValue == 0 ? false : true;
        }

        string GetTimeString(DateTime dateTime)
        {
            if (DisplayMyLocationTime())
            {
                var timeString = string.Empty;

                if (Settings.Is24Hours)
                {
                    timeString = dateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    var ttString = dateTime.ToString("tt");
                    var ampmLower = ttString.ToLower();
                    timeString = dateTime.ToString("hh:mm:ss ", CultureInfo.InvariantCulture)
                            + ampmLower;
                }
                return timeString;
            }
            else
            {
                return TimezoneTime;
            }
        }

        private bool DisplayMyLocationTime()
        {
            if (UserSettings.SelectedLocation == UserSettings.DefaultSelectedLocation
                   || string.IsNullOrEmpty(UserSettings.SelectedLocation)
                   || UserSettings.LocalTimeOnValue == (int)UserSettings.LocalTimeNotationEnum.offValue
                   || !timezoneIsValid)
            {
                return true;
            }

            return false;
        }

        string GetTimezoneTime()
        {
            var timeString = string.Empty;

            if (Settings.Is24Hours)
            {
                timeString = timezoneDateTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }
            else
            {
                timeString = timezoneDateTime.ToString("hh:mm:ss", CultureInfo.InvariantCulture);
            }
            return timeString;
        }

        string GetTemperature(double temperature)
        {
            var temperatureString = string.Empty;

            if (Settings.IsCelcius)
            {
                var temp = Conversions.ToCelcius(temperature);
                temperatureString = String.Format("{0:0.0} °C", temp);
            }
            else
            {
                temperatureString = String.Format("{0:0.0} °F", temperature);
            }
            return temperatureString;

        }

        string GetWindSpeed(double windSpeed)
        {
            var speedString = string.Empty;

            if (Settings.IsMetric)
            {
                var speed = Conversions.ToKmPerHour(windSpeed);
                speedString = String.Format("{0:0.0} km/h", speed);
            }
            else
            {
                speedString = String.Format("{0:0.0} MPH", windSpeed);
            }
            return speedString;
        }

        ObservableRangeCollection<WeatherForecastData> UpdateDailyForecast(List<WeatherDayData> dailyData)
        {
            var forecastData = new ObservableRangeCollection<WeatherForecastData>();
            foreach (WeatherDayData item in dailyData)
            {
                var day = item.Time.Day;
                var month = item.Time.Month;

                var temperatureRange = string.Empty;
                var tempMin = item.TemperatureMin;
                var tempMax = item.TemperatureMax;
                if (Settings.IsCelcius)
                {
                    var tMinCelcius = Conversions.ToCelcius(tempMin);
                    var tMaxCelcius = Conversions.ToCelcius(tempMax);
                    temperatureRange = String.Format("{0:0.0} - {1:0.0} °C", tMinCelcius, tMaxCelcius);
                }
                else
                {
                    temperatureRange = String.Format("{0:0.0} - {1:0.0} °F", tempMin, tempMax);
                }

                var windSpeed = string.Empty;
                if (Settings.IsMetric)
                {
                    var speed = Conversions.ToKmPerHour(item.WindSpeed);
                    windSpeed = String.Format("Wind: {0:0.0} km/h", speed);
                }
                else
                {
                    windSpeed = String.Format("Wind: {0:0.0} MPH", item.WindSpeed);
                }

                var imageName = GetImageNameForIcon(item.Icon, big: true);

                var forecast = new WeatherForecastData
                {
                    Day = CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)(item.Time.DayOfWeek)],
                    WeekDate = day.ToString() + " " + CultureInfo.CurrentCulture.DateTimeFormat.MonthNames[(int)month - 1],
                    TemperatureRange = temperatureRange,
                    WeekHumidityString = String.Format("{0} %", item.Humidity),
                    WeekWindSpeed = windSpeed,
                    DisplayIcon = imageName
                };
                forecastData.Add(forecast);
            }

            WeekWeatherData = forecastData;
            return WeekWeatherData;
        }

        private string GetImageNameForIcon(string iconName, bool big)
        {
            var imageName = string.Empty;
            var suffix = big ? "512.png" : ".png";

            switch (iconName)
            {
                case "cloudy":
                case "03d":
                case "03n":
                case "04d":
                case "04n":
                    imageName = "CloudyIcon";
                    break;
                case "rain":
                case "09d":
                case "10d":
                case "11d":
                case "09n":
                case "10n":
                case "11n":
                    imageName = "RainIcon";
                    break;
                case "snow":
                case "13d":
                case "13n":
                    imageName = "SnowIcon";
                    break;
                case "clear-day":
                case "01d":
                    imageName = "ClearDayIcon";
                    break;
                case "clear-night":
                case "01n":
                    imageName = "ClearNightIcon";
                    break;
                case "fog":
                case "50n":
                case "50d":
                    imageName = "FogIcon";
                    break;
                case "sleet":
                    imageName = "SleetIcon";
                    break;

                case "partly-cloudy-day":
                case "02d":
                    imageName = "PartlyCloudyIcon";
                    break;

                default:
                    imageName = "CloudyIcon";
                    break;
            }

            if (!string.IsNullOrEmpty(imageName))
            {
                imageName += suffix;
            }
            return imageName;
        }

    }
}
