using System;
using System.Drawing;

namespace Common.Entities
{
    public class Agent
    {
        public Agent(double x, double y, double angle, Color color)
        {
            X = x;
            Y = y;
            Angle = angle;
            Color = color;
            Speed = 1;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public int Speed { get; set; }
        public double Angle { get; set; }

        public void Move()
        {
            var rx = -Math.Sin(Angle);
            var ry = Math.Cos(Angle);
            X += rx * Speed;
            Y += ry * Speed;
        }

        public Color Color { get; set; }
    }
}
