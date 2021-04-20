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

        public int TimerSlider; //Fast = 42 --- Normal = 130 --- Slow = 400

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

            if (SnakeGotApple())
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

        public void StartCounter()
        {
            Timer.Interval = TimerSlider;
            Timer.Start();
        }

        public void StopCounter()
        {
            Timer.Stop();
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
                rndX = _rnd.Next(_maxFieldX);
                rndY = _rnd.Next(_maxFieldY);
                isSnake = (Cater.IsThereTheSnake(rndX, rndY));
            }
            while (isSnake);
            Apple = new Point(rndX, rndY);
            return Apple;
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
