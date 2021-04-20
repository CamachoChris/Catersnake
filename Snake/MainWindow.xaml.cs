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
        GameControl _gameControl;
        Ellipse _apple;

        public MainWindow()
        {
            InitializeComponent();
            Dispatcher.ShutdownFinished += Dispatcher_ShutdownFinished;
        }

        private void AssembleCater()
        {
            if (_graficLimbs.Count > 1)
            {
                Ellipse tmp = _graficLimbs[_graficLimbs.Count - 1];
                _graficLimbs.Insert(1, tmp);
                _graficLimbs.RemoveAt(_graficLimbs.Count - 1);
            }
            for (int i = 0; i < _graficLimbs.Count; i++)
            {
                LimbView(_gameControl.Cater.Limbs[i], i);
            }
        }

        private void LimbView(Point limb, int position)
        {
            this.Dispatcher.Invoke(() =>
            {
                if (position == 0)
                    _graficLimbs[0].Fill = Brushes.DarkGreen;
                if (position == 1)
                    _graficLimbs[1].Fill = Brushes.DarkOliveGreen;
                _graficLimbs[position].Margin = new Thickness() { Left = GridMultiplier * limb.X, Top = GridMultiplier * limb.Y };
            });
        }

        public void AppleView()
        {
            _apple.Margin = new Thickness() { Left = GridMultiplier * _gameControl.Apple.X, Top = GridMultiplier * _gameControl.Apple.Y };
        }

        private void CaterGraphicGrow()
        {
            this.Dispatcher.Invoke(() =>
            {
                Ellipse nextGraphicalLimb = new Ellipse() { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.DarkGreen };
                _graficLimbs[0].Fill = Brushes.DarkOliveGreen;
                _graficLimbs.Insert(0, nextGraphicalLimb);
                AssembleCater();
                PlayingField.Children.Add(nextGraphicalLimb);
            });
        }
    }
}
