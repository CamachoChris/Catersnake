﻿using System;
using System.Timers;
using System.Collections.Generic;
using System.Text;

namespace CatersnakeModel
{
    class GameControl
    {
        public Catersnake Cater;

        private Timer _timer = new Timer();
        public event EventHandler Tick;

        private Direction _direction;

        public GameControl(int caterStartX, int caterStartY, int maxFieldX, int maxFieldY)
        {
            Cater = new Catersnake(caterStartX, caterStartY, maxFieldX, maxFieldY);
            _direction = Direction.Right;
            _timer.Elapsed += Timer_Elapsed;
            _timer.Interval = 1000;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Cater.Move(_direction);
            if (Tick != null)
                Tick(this, EventArgs.Empty);
        }

        public void SwitchCounter()
        {
            /*if (_timer.Enabled)
                _timer.Stop();
            else*/
                _timer.Start();
        }

        public void CaterGrow()
        {
            Cater.Grow(_direction);
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
