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
                Population.Add(generateNeuroChromosomeByTemplate(networkState));
            }
        }

        private NeuroChromosome generateNeuroChromosomeByTemplate(NeuroChromosome template)
        {
            var neuroChromosome = new NeuroChromosome();
            foreach (var gen in template.Gens)
            {
                var neuroGen = gen as NeuroGen;
                var count = neuroGen.Weights.Count;
                neuroGen.Weights = new List<double>();

                for (int index = 0; index < count; index++)
                {
                    neuroGen.Weights.Add(Random.NextDouble());
                }

                var activationFuncIndex = Random.Next(1, ActivationFunctions.AvailableActivatonFunctions.Count() + 1);
                var activationFunc = ActivationFunctions.AvailableActivatonFunctions.FirstOrDefault(x => x.Index == activationFuncIndex);

                neuroGen.Value.ActivationFunc = activationFunc;
                neuroChromosome.Gens.Add(neuroGen);
            }

            return neuroChromosome;
        }

        public override void PerformIteration()
        {
            foreach (NeuroChromosome chromosome in Population)
            {
                for (int j = 0; j < TestGameEnvironment.Agents.Count; j++)
                {
                    TestGameEnvironment.Agents[j] = new EnvironmentAgent(Random.Next(90, 100), Random.Next(90, 100),
                                                                         Random.Next(0, 360), Color.Beige,
                                                                         new NeuralNetwork())
                        {
                            HarvestedFood = 1
                        };
                    TestGameEnvironment.Agents[j].Brain.SetNetworkState(chromosome);
                }

                for (var i = 0; i < 1000; i++)
                {
                    TestGameEnvironment.CalculateAgentsEnvironmentParameters();
                    TestGameEnvironment.GetHarvestedFood();

                    for (int index = 0; index < TestGameEnvironment.Agents.Count; index++)
                    {
                        var agent = TestGameEnvironment.Agents[index];
                        if (TestGameEnvironment.CanMove(agent))
                        {
                            agent.Move();
                        }
                    }
                }

                chromosome.FittnessValue = TestGameEnvironment.Agents.Sum(x => x.HarvestedFood);
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