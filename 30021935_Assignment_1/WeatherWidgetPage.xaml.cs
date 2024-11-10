using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace _30021935_Assignment_1
{
    public sealed partial class WeatherWidgetPage : Page
    {
        private const string ApiKey = "534eb92513176dfa1b65d25fc537aad2";
        private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather";

        public WeatherWidgetPage()
        {
            this.InitializeComponent();
        }

        private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
        {
            string city = CityTextBox.Text.Trim(); // Trim whitespace
            if (!string.IsNullOrWhiteSpace(city))
            {
                await FetchWeatherAsync(city);
            }
            else
            {
                CityNameTextBlock.Text = "Please enter a city name.";
                TemperatureTextBlock.Text = string.Empty;
                ConditionTextBlock.Text = string.Empty; // Reset fields
            }
        }

        private async Task FetchWeatherAsync(string city)
        {
            string url = $"{BaseUrl}?q={city},NZ&appid={ApiKey}&units=metric"; // Specify country code (NZ)
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResult = await response.Content.ReadAsStringAsync();
                        var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(jsonResult);

                        CityNameTextBlock.Text = weatherResponse.Name; // Display city name
                        TemperatureTextBlock.Text = $"{weatherResponse.Main.Temp} °C"; // Display temperature
                        ConditionTextBlock.Text = weatherResponse.Weather[0].Description; // Display conditions
                    }
                    else
                    {
                        CityNameTextBlock.Text = "City not found.";
                        TemperatureTextBlock.Text = string.Empty;
                        ConditionTextBlock.Text = string.Empty; // Reset fields
                    }
                }
                catch (Exception)
                {
                    // Handle exceptions, could be network issues or JSON parsing errors
                    CityNameTextBlock.Text = "Error fetching data.";
                    TemperatureTextBlock.Text = string.Empty;
                    ConditionTextBlock.Text = string.Empty; // Clear out fields
                    // Optionally log ex.Message for debugging
                }
            }
        }

        public class WeatherResponse
        {
            public Main Main { get; set; }
            public Weather[] Weather { get; set; }
            public string Name { get; set; }
        }

        public class Main
        {
            public double Temp { get; set; } // Temperature property
        }

        public class Weather
        {
            public string Description { get; set; } // Weather description property
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack(); // Navigate back to the previous page

        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            // Replace with the actual next page you want to navigate to
            this.Frame.Navigate(typeof(MainPage)); 
        }
    }
}