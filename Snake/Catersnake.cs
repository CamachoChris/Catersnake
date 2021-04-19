using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace CatersnakeModel
{
    public class Limb
    {
        public int X;
        public int Y;
    }
    public enum Direction
    {
        Left,
        Up,
        Right,
        Down
    }

    public class Catersnake
    {
        public List<Limb> limbs;
        private readonly int _maxFieldX, _maxFieldY;

        public Catersnake(int caterStartX, int caterStartY, int maxFieldX, int maxFieldY)
        {
            limbs = new List<Limb> { new Limb() { X = caterStartX, Y = caterStartY } };
            _maxFieldX = maxFieldX;
            _maxFieldY = maxFieldY;
        }

        public void Move(Direction direction)
        {
            SetNextHead(direction);
            limbs.RemoveAt(limbs.Count - 1);
        }

        public void Grow(Direction direction)
        {
            SetNextHead(direction);
        }

        private void SetNextHead(Direction direction)
        {
            Limb newHead = new Limb() { X = limbs[0].X, Y = limbs[0].Y};
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
            limbs.Insert(0, newHead);
        }

        public void ShowCatersnake(string txt)
        {
            Debug.WriteLine(txt);
            for (int i = 0; i < limbs.Count; i++)
            {
                Debug.WriteLine($"{i}. X={limbs[i].X}, Y={limbs[i].Y}");
            }
        }
    }
}
