using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Common.Entities;

namespace AIEngine.Entities
{
    public class GameEnvironment
    {
        public List<EnvironmentAgent> Agents { get; set; }
        public List<Food> Foods { get; set; }

        private Random Random { get; set; }
        private const int VisibleZone = 50;

        private int Width { get; set; }
        private int Height { get; set; }

        public GameEnvironment(EnvironmentOptions options)
        {
            Random = new Random((int) DateTime.Now.Ticks);
            Agents = new List<EnvironmentAgent>();
            Foods = new List<Food>();

            Width = options.FieldWidth;
            Height = options.FieldHeight;

            for (int i = 0; i < options.AgentsCount; i++)
            {
                var color = Color.FromArgb(Random.Next(0, 255), Random.Next(0, 255), Random.Next(0, 255));
                var agent = new EnvironmentAgent(Random.Next(0, Width), Random.Next(0, Height), Random.Next(0, 360),
                                                 color, options.NeuralNetwork);
                Agents.Add(agent);
            }

            for (int i = 0; i < options.FoodCount; i++)
            {
                var food = new Food(Random.Next(0, Width), Random.Next(0, Height));
                Foods.Add(food);
            }
        }

        public void GetHarvestedFood()
        {
            var harvested = new List<Food>();

            foreach (var food in Foods)
            {
                foreach (var agent in Agents)
                {
                    if (GetDistance(food.X, food.Y, agent.X, agent.Y) < 15)
                    {
                        harvested.Add(food);
                        agent.HarvestedFood++;
                    }
                }
            }

            foreach (var t in harvested)
            {
                Foods.Remove(t);
                var food = new Food(Random.Next(0, Width), Random.Next(0, Height));
                Foods.Add(food);
            }
        }

        private double GetDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + (Math.Pow(y2 - y1, 2)));
        }

        private double GetDistanceBetweenAgents(ICoordinated agent1, ICoordinated agent2)
        {
            return GetDistance(agent1.X, agent1.Y, agent2.X, agent2.Y);
        }

        public void CalculateAgentsEnvironmentParameters()
        {
            foreach (var agent in Agents)
            {
                var localAgent = agent;
                var agents = Agents.Except(new List<EnvironmentAgent> {localAgent}).ToList();
                var nearAgents =
                    agents.Where(
                        x =>
                        x != localAgent && GetDistanceBetweenAgents(x, localAgent) < VisibleZone &&
                        (localAgent.Vector.X*(x.X - localAgent.X) + localAgent.Vector.Y*(x.Y - localAgent.Y)) >= 0);

                var nearFood =
                    Foods.Where(x => GetDistanceBetweenAgents(x, localAgent) < VisibleZone &&
                                     (localAgent.Vector.X*(x.X - localAgent.X) +
                                      localAgent.Vector.Y*(x.Y - localAgent.Y)) >= 0);

                if (nearAgents.Any())
                {
                    localAgent.AgentsNear = true;
                    localAgent.DistanceToNearestAgent = agents.Min(x => GetDistanceBetweenAgents(x, localAgent));
                    localAgent.NearestAgent =
                        agents.FirstOrDefault(
                            y => GetDistanceBetweenAgents(y, localAgent) == localAgent.DistanceToNearestAgent);
                }
                else
                {
                    localAgent.AgentsNear = false;
                    localAgent.DistanceToNearestAgent = 0;
                    localAgent.NearestAgent = new Agent();
                }

                if (nearFood.Any())
                {
                    localAgent.FoodNear = true;
                    localAgent.DistanceToNearestFood = Foods.Min(x => GetDistanceBetweenAgents(x, localAgent));
                    localAgent.NearestFood =
                        Foods.FirstOrDefault(y => GetDistanceBetweenAgents(y, localAgent) == localAgent.DistanceToNearestFood);
                }
                else
                {
                    localAgent.FoodNear = false;
                    localAgent.DistanceToNearestFood = 0;
                    localAgent.NearestFood = new Food();
                }

                //agent.DistanceToNearestVertical = Math.Min(Width - agent.X, agent.X);
                //agent.DistanceToNearestHorizontal = Math.Min(Height - agent.Y, agent.Y);
            }
        }

        public bool CanMove(EnvironmentAgent agent)
        {
            var newX = agent.X + agent.Vector.X * agent.Speed;
            var newY = agent.Y + agent.Vector.Y * agent.Speed;

            if (newX < 10 || newX > Width - 10)
            {
                return false;
            }

            if (newY < 10 || newY > Height - 10)
            {
                return false;
            }

            return true;
        }
    }

    
}