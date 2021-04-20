using System;
using System.Windows;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CatersnakeModel

{
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    public class Catersnake
    {
        public List<Point> Limbs;
        private readonly int _maxFieldX, _maxFieldY;

        public Catersnake(int caterStartX, int caterStartY, int maxFieldX, int maxFieldY)
        {
            Limbs = new List<Point> { new Point() { X = caterStartX, Y = caterStartY } };
            _maxFieldX = maxFieldX;
            _maxFieldY = maxFieldY;
        }

        public void Move(Direction direction)
        {
            SetNextHead(direction);
            Limbs.RemoveAt(Limbs.Count - 1);
        }

        public void Grow(Direction direction)
        {
            SetNextHead(direction);
        }

        public bool IsThereTheSnake(int x, int y)
        {
            for (int i = 0; i < Limbs.Count; i++)
                if (x == Limbs[i].X && y == Limbs[i].Y)
                    return true;
            return false;
        }

        public bool DidCollide()
        {
            if (Limbs.Count > 4)
            {
                for (int i = 4; i < Limbs.Count; i++)
                    if (Limbs[0].X == Limbs[i].X && Limbs[0].Y == Limbs[i].Y)
                        return true;
                return false;
            }
            else
                return false;
        }

        private void SetNextHead(Direction direction)
        {
            Point newHead = new Point() { X = Limbs[0].X, Y = Limbs[0].Y};
            switch (direction)
            {
                case Direction.Left:
                    if (newHead.X == 0)
                        newHead.X = _maxFieldX - 1;
                    else
                        newHead.X--;
                    break;
                case Direction.Up:
                    if (newHead.Y == 0)
                        newHead.Y = _maxFieldY - 1;
                    else
                        newHead.Y--;
                    break;
                case Direction.Right:
                    if (newHead.X == _maxFieldX - 1)
                        newHead.X = 0;
                    else
                        newHead.X++;
                    break;
                case Direction.Down:
                    if (newHead.Y == _maxFieldY - 1)
                        newHead.Y = 0;
                    else
                        newHead.Y++;
                    break;
            }
            Limbs.Insert(0, newHead);
        }

        public void ShowCatersnake(string txt)
        {
            Debug.WriteLine(txt);
            for (int i = 0; i < Limbs.Count; i++)
            {
                Debug.WriteLine($"{i}. X={Limbs[i].X}, Y={Limbs[i].Y}");
            }
        }
    }
}
