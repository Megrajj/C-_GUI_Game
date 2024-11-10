using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI;

namespace _30021935_Assignment_1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AnimationPage : Page
    {
        private Storyboard _bounceStoryboard;
        private DoubleAnimation _bounceAnimationX;
        private DoubleAnimation _bounceAnimationY;
        public AnimationPage()
        {
            this.InitializeComponent();
            InitializeBounceAnimation();
        }
        private void InitializeBounceAnimation()
        {
            _bounceAnimationX = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            _bounceAnimationY = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = true,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseInOut }
            };

            _bounceStoryboard = new Storyboard();
            Storyboard.SetTarget(_bounceAnimationX, Ball);
            Storyboard.SetTarget(_bounceAnimationY, Ball);
            Storyboard.SetTargetProperty(_bounceAnimationX, "(Canvas.Left)");
            Storyboard.SetTargetProperty(_bounceAnimationY, "(Canvas.Top)");

            _bounceStoryboard.Children.Add(_bounceAnimationX);
            _bounceStoryboard.Children.Add(_bounceAnimationY);
            _bounceStoryboard.RepeatBehavior = RepeatBehavior.Forever; // Bouncing will repeat indefinitely
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            _bounceStoryboard.Begin(); // Start the animation
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            _bounceStoryboard.Stop(); // Stop the animation
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack(); // Navigate back to the previous page
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage)); // Adjust this to the page you want to navigate to
        }
    }
}