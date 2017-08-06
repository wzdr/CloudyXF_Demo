using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CloudyXF.Helpers;

namespace CloudyXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class SettingsPage : ContentPage
    {
        bool _isBusy = false;

        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void OnTwentyFourHoursClicked(object sender, EventArgs e)
        {
            if (_isBusy)
                return;

            _isBusy = true;
            Debug.WriteLine("OnTwentyFourHoursClicked");
            Settings.Is24Hours = !Settings.Is24Hours;
            Update24Hours();
            await Task.Delay(220);
            _isBusy = false;
        }


        private async void OnMetricClicked(object sender, EventArgs e)
        {
            if (_isBusy)
                return;

            _isBusy = true;
            Debug.WriteLine("OnMetricClicked");
            Settings.IsMetric = !Settings.IsMetric;
            UpdateMetric();
            await Task.Delay(220);
            _isBusy = false;
        }

        private async void OnCelciusClicked(object sender, EventArgs e)
        {
            if (_isBusy)
                return;

            _isBusy = true;
            Debug.WriteLine("OnCelciusClicked");
            Settings.IsCelcius = !Settings.IsCelcius;
            UpdateCelcius();
            await Task.Delay(220);
            _isBusy = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Update24Hours();
            UpdateCelcius();
            UpdateMetric();
        }

        private void Update24Hours()
        {
            if (Settings.Is24Hours)
            {
                TwentyFourHoursButton.Image = "Check_Blue_20.png";
                TwelveHoursButton.Image = null;
            }
            else
            {
                TwentyFourHoursButton.Image = null;
                TwelveHoursButton.Image = "Check_Blue_20.png";
            }
        }

        private void UpdateCelcius()
        {
            if (Settings.IsCelcius)
            {
                CelciusButton.Image = "Check_Blue_20.png";
                FahrenheitButton.Image = null;
            }
            else
            {
                CelciusButton.Image = null;
                FahrenheitButton.Image = "Check_Blue_20.png";
            }
        }

        private void UpdateMetric()
        {
            if (Settings.IsMetric)
            {
                MetricButton.Image = "Check_Blue_20.png";
                ImperialButton.Image = null;
            }
            else
            {
                MetricButton.Image = null;
                ImperialButton.Image = "Check_Blue_20.png";
            }
        }
    }
}