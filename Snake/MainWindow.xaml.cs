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
using CatersnakeModel;
using System.Diagnostics;

namespace CatersnakeApp
{
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
        const int SnakeStartX = 13;
        const int SnakeStartY = 13;

        List<Ellipse> _graficLimbs;
        GameControl _gameControl = new GameControl(SnakeStartX, SnakeStartY, PlayingFieldWidth, PlayingFieldHeight);
        

        public MainWindow()
        {
            InitializeComponent();
            _graficLimbs = new List<Ellipse> { new Ellipse { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.DarkGreen } };
            _gameControl.Tick += GameControl_Tick;
            _gameControl.LetHimGrow += GameControl_LetHimGrow;
            PlayingField.Children.Add(_graficLimbs[0]);
        }

        private void GameControl_LetHimGrow(object sender, EventArgs e)
        {
            CaterGraphicGrow();
        }

        private void GameControl_Tick(object sender, EventArgs e)
        {
            AssembleCater();
        }

        private void AssembleCater()
        {
            for (int i = 0; i < _graficLimbs.Count; i++)
                PaintLimb(_gameControl.Cater.Limbs[i], i);
        }

        private void PaintLimb(Limb limb, int position)
        {
            this.Dispatcher.Invoke(() =>
            {
                _graficLimbs[position].Margin = new Thickness() { Left = GridMultiplier * limb.X, Top = GridMultiplier * limb.Y };
            });                
        }

        private void CaterGraphicGrow()
        {
            this.Dispatcher.Invoke(() =>
            {
                Ellipse nextGraphicalLimb = new Ellipse() { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.DarkGreen };
                _graficLimbs.Add(nextGraphicalLimb);
                AssembleCater();
                PlayingField.Children.Add(nextGraphicalLimb);
            });

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
            AssembleCater();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.Key)
            {
                case Key.Left:
                    _gameControl.ChangeDirection(Direction.Left);
                    break;
                case Key.Up:
                    _gameControl.ChangeDirection(Direction.Up);
                    break;
                case Key.Right:
                    _gameControl.ChangeDirection(Direction.Right);
                    break;
                case Key.Down:
                    _gameControl.ChangeDirection(Direction.Down);
                    break;
                case Key.Space:
                    _gameControl.CaterGrow();
                    break;
            }
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            _gameControl.SwitchCounter();
        }
    }
}
