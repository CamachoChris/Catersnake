using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitModel();
            InitView();
            CreateSnakeForPreview();
            AppleView();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            ResetModel();
            ResetView();
            InitGame();
            _gameControl.TimerSlider = (int)slider.Value;
            _gameControl.StartCounter();
        }

        private void GameControl_Tick(object sender, EventArgs e)
        {
            AssembleCater();
        }

        private void GameControl_LetHimGrow(object sender, EventArgs e)
        {
            CaterGraphicGrow();

            this.Dispatcher.Invoke(() =>
            {
                AppleView();
            });
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
            }
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, $"{appName}\n{version}\n{timeOfDevelopment} {developer}.\nNo rights reserved...", $"About {appName}");
        }

        private void MenuQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Dispatcher.BeginInvokeShutdown(System.Windows.Threading.DispatcherPriority.Send);
            Dispatcher.InvokeShutdown();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Dispatcher_ShutdownFinished(object sender, EventArgs e)
        {
            ResetModel();
            ResetView();
            Application.Current.Shutdown();
        }
    }
}
