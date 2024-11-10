using System;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace _30021935_Assignment_1
{
    public sealed partial class Snake_Game : Page
    {
        private const int SnakeSquareSize = 20;
        private const int GameWidth = 1000;
        private const int GameHeight = 500;

        private readonly List<Rectangle> snake = new List<Rectangle>();
        private readonly DispatcherTimer gameTimer = new DispatcherTimer();
        private readonly Random rand = new Random();

        private int dx = 0, dy = 0;
        private int score = 0;
        private Rectangle food;

        // List of fruits with their colors
        private readonly Dictionary<string, SolidColorBrush> fruits = new Dictionary<string, SolidColorBrush>
        {
            { "Apple", new SolidColorBrush(Colors.Red) },
            { "Banana", new SolidColorBrush(Colors.Yellow) },
            { "Grape", new SolidColorBrush(Color.FromArgb(255, 128, 0, 128)) }, // Purple
            { "Orange", new SolidColorBrush(Colors.Orange) },
            { "Watermelon", new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)) } // Green for the outside
        };

        public Snake_Game()
        {
            this.InitializeComponent();
            InitializeGame();
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
        }

        private void InitializeGame()
        {
            gameTimer.Interval = TimeSpan.FromMilliseconds(100);
            gameTimer.Tick += UpdateGame;
            StartNewGame();
        }

        private void StartNewGame()
        {
            // Ensure GameCanvas and UI components are initialized
            if (GameCanvas == null || ScoreText == null)
            {
                throw new InvalidOperationException("GameCanvas or ScoreText is not initialized.");
            }

            foreach (var part in snake)
            {
                GameCanvas.Children.Remove(part);
            }
            snake.Clear();

            Rectangle head = new Rectangle
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = new SolidColorBrush(Colors.Orange),
            };

            GameCanvas.Children.Add(head);
            Canvas.SetTop(head, 100);
            Canvas.SetLeft(head, 100);
            snake.Add(head);

            dx = SnakeSquareSize; // Moving to the right
            dy = 0;
            score = 0;
            ScoreText.Text = "Score: 0";

            PlaceFood();
            gameTimer.Start();

            // Hide buttons when the game starts
            HomeButton.Visibility = Visibility.Collapsed;
            PlayAgainButton.Visibility = Visibility.Collapsed;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            switch (args.VirtualKey)
            {
                case Windows.System.VirtualKey.Right: if (dx == 0) { dx = SnakeSquareSize; dy = 0; } break;
                case Windows.System.VirtualKey.Left: if (dx == 0) { dx = -SnakeSquareSize; dy = 0; } break;
                case Windows.System.VirtualKey.Up: if (dy == 0) { dx = 0; dy = -SnakeSquareSize; } break;
                case Windows.System.VirtualKey.Down: if (dy == 0) { dx = 0; dy = SnakeSquareSize; } break;
            }
        }

        private async void UpdateGame(object sender, object e)
        {
            try
            {
                // Move the snake
                for (int i = snake.Count - 1; i > 0; i--)
                {
                    Canvas.SetLeft(snake[i], Canvas.GetLeft(snake[i - 1]));
                    Canvas.SetTop(snake[i], Canvas.GetTop(snake[i - 1]));
                }

                Canvas.SetLeft(snake[0], Canvas.GetLeft(snake[0]) + dx);
                Canvas.SetTop(snake[0], Canvas.GetTop(snake[0]) + dy);

                if (CheckCollision())
                {
                    gameTimer.Stop();

                    // Show pop-up box with score
                    ContentDialog gameOverDialog = new ContentDialog
                    {
                        Title = "Game Over",
                        Content = $"Your score is {score}.",
                        PrimaryButtonText = "Play Again",
                        CloseButtonText = "Go to Home"
                    };

                    gameOverDialog.PrimaryButtonClick += (s, ev) => StartNewGame();
                    gameOverDialog.CloseButtonClick += (s, ev) => Frame.Navigate(typeof(MainPage)); // Adjust for your actual home page

                    await gameOverDialog.ShowAsync();
                }
                else if (CheckFoodCollision())
                {
                    score++;
                    ScoreText.Text = "Score: " + score;
                    PlaceFood();
                    Rectangle newPart = new Rectangle
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                        Fill = new SolidColorBrush(Colors.Orange),
                    };
                    snake.Add(newPart);
                    GameCanvas.Children.Add(newPart);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and show a message if needed
                System.Diagnostics.Debug.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private void PlaceFood()
        {
            if (food != null)
            {
                GameCanvas.Children.Remove(food);
            }

            // Choose a random fruit from the dictionary
            var fruitTypes = new List<string>(fruits.Keys);
            string randomFruit = fruitTypes[rand.Next(fruitTypes.Count)];
            SolidColorBrush fruitColor = fruits[randomFruit];

            food = new Rectangle
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = fruitColor,
            };
            GameCanvas.Children.Add(food);

            int foodLeft = rand.Next(0, GameWidth / SnakeSquareSize) * SnakeSquareSize;
            int foodTop = rand.Next(0, GameHeight / SnakeSquareSize) * SnakeSquareSize;
            Canvas.SetLeft(food, foodLeft);
            Canvas.SetTop(food, foodTop);
        }

        private bool CheckFoodCollision()
        {
            double snakeX = Canvas.GetLeft(snake[0]);
            double snakeY = Canvas.GetTop(snake[0]);

            return snakeX == Canvas.GetLeft(food) && snakeY == Canvas.GetTop(food);
        }

        private bool CheckCollision()
        {
            double snakeX = Canvas.GetLeft(snake[0]);
            double snakeY = Canvas.GetTop(snake[0]);

            // Check for wall collisions
            if (snakeX < 0 || snakeY < 0 || snakeX >= GameWidth || snakeY >= GameHeight)
                return true;

            // Check for self-collisions
            for (int i = 1; i < snake.Count; i++)
            {
                if (snakeX == Canvas.GetLeft(snake[i]) && snakeY == Canvas.GetTop(snake[i]))
                    return true;
            }

            return false;
        }

        // Event handler for Home Button
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage)); // Adjust the page name to your home page
        }

        // Event handler for Play Again Button
        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {
            PlayAgainButton.Visibility = Visibility.Collapsed; // Hide the Play Again button
            HomeButton.Visibility = Visibility.Collapsed;       // Hide the Home button
            StartNewGame(); // Restart the game
        }
    }
}
