using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace CloudyXF.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

        #region Setting Constants
        private const string CityKey = "city_key";
        private static readonly string CityDefault = string.Empty;

        private const string Is24HoursKey = "is_24Hours";
        private static readonly bool Is24HoursDefault = true;

        private const string IsCelciusKey = "is_celcius";
        private static readonly bool IsCelciusDefault = true;

        private const string IsMetricKey = "is_metric";
        private static readonly bool IsMetricDefault = true;

        #endregion

        public static string SelectedCity
        {
            get { return AppSettings.GetValueOrDefault(CityKey, CityDefault); }
            set { AppSettings.AddOrUpdateValue(CityKey, value); }
        }

        public static bool Is24Hours
        {
            get { return AppSettings.GetValueOrDefault(Is24HoursKey, Is24HoursDefault); }
            set { AppSettings.AddOrUpdateValue(Is24HoursKey, value); }
        }

        public static bool IsCelcius
        {
            get { return AppSettings.GetValueOrDefault(IsCelciusKey, IsCelciusDefault); }
            set { AppSettings.AddOrUpdateValue(IsCelciusKey, value); }
        }

        public static bool IsMetric
        {
            get { return AppSettings.GetValueOrDefault(IsMetricKey, IsMetricDefault); }
            set { AppSettings.AddOrUpdateValue(IsMetricKey, value); }
        }



    }
}