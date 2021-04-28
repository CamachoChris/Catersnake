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
using System.Windows.Threading;
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
            PositionAppleView();
        }

        private void StartGameButton_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
            InitGame();
            _gameControl.StartTimer((int)slider.Value);

            EatenTextBlock.Text = string.Format("0");
            _caterMoves = true;
        }

        /// <summary>
        /// Using Dispatcher.BeginInvoke
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameControl_Tick(object sender, EventArgs e)
        {
            if (!_caterMoves) return;

            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                CaterIsMovingView();
            }));
        }

        /// <summary>
        /// Using Dispatcher.BeginInvoke
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GameControl_LetHimGrow(object sender, EventArgs e)
        {
            if (!_caterMoves) return;

            this.Dispatcher.BeginInvoke((Action)(() =>
            {
                GrowHeadView();
                CaterIsMovingView();
                PositionAppleView();
                EatenTextBlock.Text = string.Format($"{(int.Parse(EatenTextBlock.Text) + 1)}");
            }));
        }

        private void GameControl_SelfEaten(object sender, EventArgs e)
        {
            _caterMoves = false;
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
            MessageBox.Show(this, $"{AppName}\n{Version}\n{TimeOfDevelopment} {Developer}.\nNo rights reserved...", $"About {AppName}");
        }

        private void MenuQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _caterMoves = false;
            Dispatcher.BeginInvokeShutdown(System.Windows.Threading.DispatcherPriority.Send);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
        }

        private void Dispatcher_ShutdownFinished(object sender, EventArgs e)
        {
            ResetGame();
            Application.Current.Shutdown();
        }
    }
}
