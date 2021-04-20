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
        const string AppName= "Catersnake";
        const string Version = "0.1.0";
        const string Developer = "Grimakar";
        const string TimeOfDevelopment = "April 2021";

        const int CaterThickness = 24;
        const int GridMultiplier = 20;

        const int PlayingFieldWidth = 25;
        const int PlayingFieldHeight = 25;
        const int SnakeStartX = 13;
        const int SnakeStartY = 13;

        private List<Ellipse> _limbsView;
        private GameControl _gameControl;
        private Ellipse _apple;

        public MainWindow()
        {
            InitializeComponent();
            Dispatcher.ShutdownFinished += Dispatcher_ShutdownFinished;
        }

        private void MoveTailToNeckView()
        {
            if (_limbsView.Count > 1)
            {
                Ellipse tmp = _limbsView[_limbsView.Count - 1];
                _limbsView.Insert(1, tmp);
                _limbsView.RemoveAt(_limbsView.Count - 1);
            }
        }

        private void PositionCaterView()
        {
            for (int i = 0; i < _limbsView.Count; i++)
            {
                _limbsView[i].Margin = new Thickness() 
                {
                    Left = GridMultiplier * _gameControl.Cater.Limbs[i].X,
                    Top = GridMultiplier * _gameControl.Cater.Limbs[i].Y 
                };
            }
        }

        private void CaterIsMovingView()
        {
            MoveTailToNeckView();
            PositionCaterView();
        }

        private void GrowHeadView()
        {
            Ellipse nextLimb = new Ellipse() { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.DarkGreen };
            _limbsView[0].Fill = Brushes.DarkOliveGreen;
            _limbsView.Insert(0, nextLimb);
            PlayingField.Children.Add(nextLimb);
        }

        private void PositionAppleView()
        {
            _apple.Margin = new Thickness() { Left = GridMultiplier * _gameControl.Apple.X, Top = GridMultiplier * _gameControl.Apple.Y };
        }
    }
}
