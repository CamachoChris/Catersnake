using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SnakeModel;

namespace CatersnakeApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        const string appName= "Catersnake";
        const string version = "0.0.0";
        const string developer = "Grimakar";
        const string timeOfDevelopment = "April 2021";

        const int CaterThickness = 24;
        const int GridMultiplier = 20;

        const int PlayingFieldWidth = 25;
        const int PlayingFieldHeight = 25;

        Catersnake snake = new Catersnake(13, 13, PlayingFieldWidth, PlayingFieldHeight);
        Ellipse circle;

        public MainWindow()
        {
            InitializeComponent();
            circle = new Ellipse() { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.DarkGreen };
            PlayingField.Children.Add(circle);

        }

        private void PaintCater()
        {
            PaintLimb(snake.limbs[0].X, snake.limbs[0].Y);
        }

        private void PaintLimb(int column, int row)
        {
            circle.Margin = new Thickness() { Left = GridMultiplier * column, Top= GridMultiplier * row };
        }

        private void MenuQuit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, $"{appName}\n{version}\n{timeOfDevelopment} {developer}.\nNo rights reserved...", $"About {appName}");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PaintCater();
            /*snake.Grow(Direction.Left);
            snake.Grow(Direction.Down);
            snake.Grow(Direction.Right);
            snake.Grow(Direction.Right);*/
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    snake.Move(Direction.Left);
                    break;
                case Key.Up:
                    snake.Move(Direction.Up);
                    break;
                case Key.Right:
                    snake.Move(Direction.Right);
                    break;
                case Key.Down:
                    snake.Move(Direction.Down);
                    break;
            }
            PaintCater();
        }
    }
}
