using System;
using System.Windows;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace CatersnakeModel
{
    class GameControl
    {
        public Catersnake Cater;
        public Point Apple = new Point();

        private Timer _timer = new Timer();
        public event EventHandler Tick;
        public event EventHandler LetHimGrow;

        private Direction _direction;

        Random rnd = new Random();

        private int _maxFieldX, _maxFieldY;

        public GameControl(int caterStartX, int caterStartY, int maxFieldX, int maxFieldY)
        {
            Cater = new Catersnake(caterStartX, caterStartY, maxFieldX, maxFieldY);
            _maxFieldX = maxFieldX;
            _maxFieldY = maxFieldY;
            _direction = Direction.Right;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 100;
        }

        public bool SnakeGotApple()
        {
            if (Cater.Limbs[0].X == Apple.X && Cater.Limbs[0].Y == Apple.Y)
                return true;
            return false;
        }

        public Point PlaceApple()
        {
            int rndX, rndY;
            bool isSnake;
            do
            {
                rndX = rnd.Next(_maxFieldX);
                rndY = rnd.Next(_maxFieldY);
                isSnake = (Cater.IsThereTheSnake(rndX, rndY));
            }
            while (isSnake);
            Apple = new Point(rndX, rndY);
            return Apple;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (SnakeGotApple())
            {
                Cater.Grow(_direction);
                if (LetHimGrow != null)
                    LetHimGrow(this, EventArgs.Empty);
            }
            else
            {
                Cater.Move(_direction);
                if (Tick != null)
                    Tick(this, EventArgs.Empty);
            }
            if (Cater.DidCollide())
                _timer.Stop();
        }

        public void StartCounter()
        {
            _timer.Start();
        }

        public void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (_direction != Direction.Right)
                        _direction = Direction.Left;
                    break;
                case Direction.Up:
                    if (_direction != Direction.Down)
                        _direction = Direction.Up;
                    break;
                case Direction.Right:
                    if (_direction != Direction.Left)
                        _direction = Direction.Right;
                    break;
                case Direction.Down:
                    if (_direction != Direction.Up)
                        _direction = Direction.Down;
                    break;
            }
        }

        public Direction GetDirection()
        {
            return _direction;
        }
    }
}
