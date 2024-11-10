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
using Windows.Media.SpeechSynthesis;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace _30021935_Assignment_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TextGeneratorPage : Page
    {
        private SpeechSynthesizer _speechSynthesizer;
        private MediaElement _mediaElement;

        public TextGeneratorPage()
        {
            this.InitializeComponent();
            _speechSynthesizer = new SpeechSynthesizer();
            _mediaElement = new MediaElement();
        }
        private async void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            var text = InputTextBox.Text;
            if (!string.IsNullOrWhiteSpace(text))
            {
                var stream = await _speechSynthesizer.SynthesizeTextToStreamAsync(text);
                _mediaElement.SetSource(stream, stream.ContentType);
                _mediaElement.Play();
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _mediaElement.Stop();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            // Replace `NextPageType` with the type of the next page to navigate to
            this.Frame.Navigate(typeof(WeatherWidgetPage));
        }
    }
}
