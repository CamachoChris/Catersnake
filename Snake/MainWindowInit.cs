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
        private void InitGame()
        {
            InitModel();
            InitView();
            AssembleCater();
            AppleView();
        }

        private void InitModel()
        {
            _gameControl = new GameControl(SnakeStartX, SnakeStartY, PlayingFieldWidth, PlayingFieldHeight);
            _gameControl.Tick += GameControl_Tick;
            _gameControl.LetHimGrow += GameControl_LetHimGrow;
        }

        private void InitView()
        {
            _graficLimbs = new List<Ellipse> { new Ellipse { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.DarkOliveGreen } };
            PlayingField.Children.Add(_graficLimbs[0]);

            _apple = new Ellipse { Height = CaterThickness, Width = CaterThickness, Fill = Brushes.Red };
            PlayingField.Children.Add(_apple);
        }

        private void ResetModel()
        {
            _gameControl.Timer.Stop();
            _gameControl.Timer.Close();
            _gameControl.Tick -= GameControl_Tick;
            _gameControl.LetHimGrow -= GameControl_LetHimGrow;

            _gameControl = null;
        }

        private void ResetView()
        {
            PlayingField.Children.Clear();
            _graficLimbs = null;
            _apple = null;
        }

        private void CreateSnakeForPreview()
        {
            _gameControl.Cater.Grow(Direction.Left);
            _gameControl.Cater.Grow(Direction.Down);
            _gameControl.Cater.Grow(Direction.Down);
            for (int i = 0; i < 4; i++)
                _gameControl.Cater.Grow(Direction.Right);
            for (int i = 0; i < 5; i++)
                _gameControl.Cater.Grow(Direction.Up);
            for (int i = 0; i < 5; i++)
                _gameControl.Cater.Grow(Direction.Left);
            for (int i = 0; i < _gameControl.Cater.Limbs.Count - 1; i++)
                CaterGraphicGrow();
            _gameControl.Apple.X = 9;
            _gameControl.Apple.Y = 10;
        }
    }
}
