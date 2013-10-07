using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using AIEngine.Entities;
using GeneticAlgorithm;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation.NeuroGeneticAlgorithm
{
    public class NeuroGeneticAlgorithm : GeneticAlgorithm<Neuron>
    {
        private const int TestPopulationCount = 10;
        public GameEnvironment TestGameEnvironment { get; set; }
        private NeuralNetwork NeuralNetwork { get; set; }
        private Random Random { get; set; }

        public NeuroGeneticAlgorithm(IFittnessFunction<Neuron> fittnessFunction, ISelection<Neuron> selection,
                                     ICrossover<Neuron> crossover, IMutation<Neuron> mutation,
                                     ITerminate<Neuron> terminate, NeuralNetwork networkForTeach)
            : base(fittnessFunction, selection, crossover, mutation, terminate)
        {
            NeuralNetwork = networkForTeach;
            Random = new Random((int) DateTime.Now.Ticks);
            generateNeuroPopulation_();

            var options = new EnvironmentOptions
                {
                    AgentsCount = Population.Count,
                    FoodCount = 5,
                    FieldWidth = 200,
                    FieldHeight = 200
                };

            TestGameEnvironment = new GameEnvironment(options);
        }

        private void generateNeuroPopulation_()
        {
            var networkState = NeuralNetwork.GetNetworkState();

            for (var i = 0; i < TestPopulationCount; i++)
            {
                var neuroChromosome = new List<IGen<Neuron>>();
                foreach (var layer in networkState)
                {
                    foreach (var neuron in layer.Neurons)
                    {
                        var neuroGen = new NeuroGen();
                        var weights = layer.Links.Count(x => x.Target.Index == neuron.Index);

                        for (int index = 0; index < weights; index++)
                        {
                            neuroGen.Weights.Add(Random.NextDouble());
                        }

                        var activationFuncIndex = Random.Next(1,
                                                              ActivationFunctions.AvailableActivatonFunctions.Count() +
                                                              1);
                        var activationFunc = ActivationFunctions.AvailableActivatonFunctions.FirstOrDefault(x => x.Index == activationFuncIndex);

                        neuroGen.Value = new Neuron(activationFunc) {Index = neuron.Index, Parent = layer};
                        neuroChromosome.Add(neuroGen);
                    }
                }

                Population.Add(new NeuroChromosome { Gens = neuroChromosome, FittnessValue = 0 });
            }
        }

        public override void PerformIteration()
        {
            for (int i = 0; i < Population.Count; i++)
            {
                TestGameEnvironment.Agents[i] = new EnvironmentAgent(Random.Next(90, 100), Random.Next(90, 100), Random.Next(0, 360),Color.Beige, new NeuralNetwork())
                    {
                        HarvestedFood = 1
                    };

                TestGameEnvironment.Agents[i].Brain.SetNetworkState(Population[i] as NeuroChromosome);
            }


            for (var i = 0; i < 10000; i++)
            {
                TestGameEnvironment.CalculateAgentsEnvironmentParameters();
                TestGameEnvironment.GetHarvestedFood();

                foreach (var agent in TestGameEnvironment.Agents)
                {
                    if (TestGameEnvironment.CanMove(agent))
                    {
                        agent.Move();
                    }
                }
            }

            for (int index = 0; index < Population.Count; index++)
            {
                var chromosome = Population[index];
                chromosome.FittnessValue = TestGameEnvironment.Agents[index].HarvestedFood;
            }

            var selection = Selection.Select(Population);
            var crossover = Crossover.Perform(selection);

            foreach (var chromosome in crossover)
            {
                Mutation.Mutate(chromosome);
            }

            Population.Remove(Population.FirstOrDefault(
                x => x.FittnessValue == Population.Min(y => y.FittnessValue)));
            Population.Remove(Population.FirstOrDefault(
                x => x.FittnessValue == Population.Min(y => y.FittnessValue)));

            Population.AddRange(crossover);
        }
    }
}