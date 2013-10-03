using System;
using System.Collections.Generic;
using System.Drawing;
using Common.Entities;
using NeuralNetworkCore;

namespace AIEngine.Entities
{
    public class EnvironmentAgent : Agent
    {
        public EnvironmentAgent(double x, double y, double angle, Color color, NeuralNetwork brain) : base(x, y, angle, color, brain)
        {
            AgentsNear = false;
            FoodNear = false;
            NearestAgent = new Agent();
            NearestFood = new Food();
        }

        public double AngleToFood
        {
            get
            {
                var vectorToFood = getVector_(this, NearestFood);
                return getAngleBetweenVectors_(vectorToFood, Vector);
            }
        }

        public double AngleToAgent
        {
            get
            {
                var vectorToAgent = getVector_(this, NearestAgent);
                return getAngleBetweenVectors_(vectorToAgent, Vector);
            }
        }

        public Food NearestFood { get; set; }

        public Agent NearestAgent { get; set; }

        public bool FoodNear { get; set; }

        public bool AgentsNear { get; set; }

        public double DistanceToNearestAgent { get; set; }

        public double DistanceToNearestFood { get; set; }

        public int HarvestedFood { get; set; }

        private Vector getVector_(ICoordinated first, ICoordinated second)
        {
            if (second == null || first == null)
            {
                return new Vector();
            }

            return new Vector
                {
                    X = second.X - first.X,
                    Y = second.Y - first.Y
                };
        }

        private double getAngleBetweenVectors_(Vector first, Vector second)
        {
            return (first.X*second.X + first.Y*second.Y)/
                   (Math.Sqrt(Math.Pow(first.X, 2) + Math.Pow(first.Y, 2))*
                    Math.Sqrt(Math.Pow(second.X, 2) + Math.Pow(second.Y, 2)));
        }

        public override void Move()
        {
            var foodNear = FoodNear ? 0.7 : 0.3;
            var agentsNear = AgentsNear ? 0.7 : 0.3;
            var result = Brain.Activate(new List<double> { Angle, DistanceToNearestFood, foodNear, AngleToFood, DistanceToNearestAgent, AngleToAgent, agentsNear });
            Angle = result[0];

            base.Move();
        }
    }
}