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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace _30021935_Assignment_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void SnakeGameButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Memory Match game page
            this.Frame.Navigate(typeof(Snake_Game));
        }

        private void AnimationButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Animations page
            this.Frame.Navigate(typeof(AnimationPage));
        }

        private void TextGeneratorButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Text Generator page
            this.Frame.Navigate(typeof(TextGeneratorPage));
        }

        private void WeatherWidgetButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the Text Generator page
            this.Frame.Navigate(typeof(WeatherWidgetPage));
        }
    }
}
