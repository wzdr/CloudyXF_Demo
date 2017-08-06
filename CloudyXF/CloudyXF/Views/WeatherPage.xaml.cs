using Xamarin.Forms;
using System.Diagnostics;

using CloudyXF.ViewModels;
using CloudyXF.Models;
using MvvmHelpers;

using Xamarin.Forms.Xaml;
//using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
//using Platform = Xamarin.Forms.PlatformConfiguration;


[assembly: XamlCompilation(XamlCompilationOptions.Skip)]
namespace CloudyXF.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        WeatherViewController weatherViewController;
        WeatherPageViewModel weatherPageViewModel;

        ObservableRangeCollection<WeatherForecastData> weatherForecast;

        public WeatherPage()
        {
            InitializeComponent();

            weatherViewController = new WeatherViewController();
            weatherPageViewModel = new WeatherPageViewModel();

            this.BindingContext = weatherPageViewModel;
            weatherForecast = new ObservableRangeCollection<WeatherForecastData>();
        //now set in XAML    ListViewWeather.ItemsSource = weatherForecast;

            ListViewWeather.Refreshing += async (sender, e) =>
            {
                ((Xamarin.Forms.ListView)sender).SelectedItem = null;
                var weatherData = await weatherViewController.ExecuteGetWeatherCommand();
                //TimeLabel.Text = weatherPageViewModel.TimeString;
                ListViewWeather.IsRefreshing = false;
                weatherForecast = weatherPageViewModel.UpdateUi(weatherData);
            };

            GetWeatherButton.Clicked += async (sender, e) =>
            {
                if (!weatherPageViewModel.IsBusy)
                {
                    weatherPageViewModel.IsBusy = true;
                    ListViewWeather.IsRefreshing = true;
                    var weatherData = await weatherViewController.ExecuteGetWeatherCommand();
                    ListViewWeather.IsRefreshing = false;
                    weatherPageViewModel.IsBusy = false;
                    weatherForecast = weatherPageViewModel.UpdateUi(weatherData);
                }
            };

            ToolbarItems.Add(new ToolbarItem("Settings", "GearIcon29.png", async () =>
            {
                Debug.WriteLine("ToolbarItem clicked");
                await Navigation.PushAsync(new SettingsPage());
            }));
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (!weatherPageViewModel.IsBusy)
            {
                weatherPageViewModel.IsBusy = true;
                ListViewWeather.IsRefreshing = true;
                var weatherData = await weatherViewController.ExecuteGetWeatherCommand();
                weatherPageViewModel.IsBusy = false;
                ListViewWeather.IsRefreshing = false;
                weatherForecast = weatherPageViewModel.UpdateUi(weatherData);
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            // Manually deselect item
            ListViewWeather.SelectedItem = null;
        }
    }
}