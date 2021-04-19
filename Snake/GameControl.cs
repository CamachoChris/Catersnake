using System;
using System.Collections.Generic;
using System.Text;

namespace CatersnakeModel
{
    class GameControl
    {
        public Catersnake Cater;

        private Direction _direction;

        public GameControl(int caterStartX, int caterStartY, int maxFieldX, int maxFieldY)
        {
            Cater = new Catersnake(caterStartX, caterStartY, maxFieldX, maxFieldY);
            _direction = Direction.Right;
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
            Cater.Move(_direction);
        }

        public Direction GetDirection()
        {
            return _direction;
        }
    }
}
