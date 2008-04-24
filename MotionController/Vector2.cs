using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Robotics.CoroBot.MotionController
{
    public class Vector2
    {
        private double x, y;
        public double X { get { return x; } set { x = value; } }
        public double Y { get { return y; } set { y = value; } }
        public double Norm { get { return Math.Sqrt(x * x + y * y); } }
        public double Angle { get { return Math.Atan2(y, x); } }

        public Vector2 Clone()
        {
            return new Vector2(x, y);
        }

        public Vector2 Add(Vector2 v)
        {
            return new Vector2(x + v.X, y + v.Y);
        }

        public Vector2 Subtract(Vector2 v)
        {
            return new Vector2(x - v.X, y - v.Y);
        }

        public Vector2(PointF pointf)
            : this(pointf.X, pointf.Y)
        {
            
        }

        public Vector2(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public String ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }
}
