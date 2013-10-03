using System;
using System.Drawing;
using Common.Entities;
using NeuralNetworkCore;

namespace AIEngine.Entities
{
    public class Agent : ICoordinated
    {
        public Agent(double x, double y, double angle, Color color, NeuralNetwork brain)
        {
            X = x;
            Y = y;
            Angle = angle;
            Color = color;
            Speed = 1;
            Brain = brain;

            _vector = new Vector();
        }

        public Agent()
        {
            _vector = new Vector();
        }

        public double X { get; set; }
        public double Y { get; set; }

        public NeuralNetwork Brain { get; set; }
        public int Speed { get; set; }
        public double Angle { get; set; }

        private readonly Vector _vector;

        public Vector Vector
        {
            get
            {
                _vector.X = -Math.Sin(Angle);
                _vector.Y = Math.Cos(Angle);
                return _vector;
            }
        }

        public virtual void Move()
        {
            X += Vector.X * Speed;
            Y += Vector.Y * Speed;
        }

        public Color Color { get; set; }

        public override string ToString()
        {
            return string.Format("X: {0} , Y: {1}, A: {2}, V: {3}", X, Y, Angle, Vector);
        }
    }
}
