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

        public Timer Timer = new Timer();

        public event EventHandler Tick;
        public event EventHandler LetHimGrow;

        private Direction _direction = Direction.Left;
        private Direction _favoredDirection = Direction.Left;

        readonly private Random _rnd = new Random();

        readonly private int _maxFieldX, _maxFieldY;

        public GameControl(int caterStartX, int caterStartY, int maxFieldX, int maxFieldY)
        {
            Cater = new Catersnake(caterStartX, caterStartY, maxFieldX, maxFieldY);
            _maxFieldX = maxFieldX;
            _maxFieldY = maxFieldY;

            Timer.Elapsed += Timer_Elapsed;
            PlaceApple();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _direction = _favoredDirection;

            if (HasCaterApple())
            {
                PlaceApple();

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
                Timer.Stop();
        }

        public void StartTimer(int interval)
        {
            Timer.Interval = interval;
            Timer.Start();
        }

        public bool HasCaterApple()
        {
            if (Cater.Limbs[0].X == Apple.X && Cater.Limbs[0].Y == Apple.Y)
                return true;
            return false;
        }

        public void PlaceApple()
        {
            int rndX, rndY;
            bool isCater;

            do
            {
                rndX = _rnd.Next(_maxFieldX);
                rndY = _rnd.Next(_maxFieldY);
                isCater = (Cater.IsThereTheCater(rndX, rndY));
            }
            while (isCater);

            Apple.X = rndX;
            Apple.Y = rndY;
        }

        public void ChangeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (_direction != Direction.Right)
                        _favoredDirection = Direction.Left;
                    break;
                case Direction.Up:
                    if (_direction != Direction.Down)
                        _favoredDirection = Direction.Up;
                    break;
                case Direction.Right:
                    if (_direction != Direction.Left)
                        _favoredDirection = Direction.Right;
                    break;
                case Direction.Down:
                    if (_direction != Direction.Up)
                        _favoredDirection = Direction.Down;
                    break;
            }
        }
    }
}
