using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Exceptions;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class ComplexNeuroCrossover : ICrossover<Neuron>
    {
        private int DotsCount { get; set; }
        private bool ShuffleActivationFunctions { get; set; }
        private bool ShuffleWeights { get; set; }
        private Random Random { get; set; }

        public ComplexNeuroCrossover(int dotsCount, bool shuffleActivationFunctions, bool shuffleWeights)
        {
            DotsCount = dotsCount;
            ShuffleActivationFunctions = shuffleActivationFunctions;
            ShuffleWeights = shuffleWeights;
            Random = new Random((int) DateTime.Now.Ticks);
        }

        public List<IChromosome<Neuron>> Perform(List<IChromosome<Neuron>> population)
        {
            if (population.Count > 2)
            {
                throw new InvalidArgumentCountException(
                    "Invalid argument count provided to the Perform Crossover function.");
            }

            var firstChrom = population.First() as NeuroChromosome;
            var secondChrom = population.Last() as NeuroChromosome;

            if (firstChrom.Gens.Count != secondChrom.Gens.Count)
            {
                throw new InvalidArgumentCountException("The gens count is not equal in Perform Crossover function.");
            }

            var firstChild = new NeuroChromosome();
            var secondChild = new NeuroChromosome();

            var part = firstChrom.Gens.Count / DotsCount;

            for (int i = 0, j = 0; i < firstChrom.Gens.Count; i+=part, j++)
            {
                var firstGensPart = firstChrom.Gens.Skip(i).Take(part).ToList();
                var secondGensPart = secondChrom.Gens.Skip(i).Take(part).ToList();

                if (j % 2 != 0)
                {
                    crossoverNeuroChromosome_(firstGensPart, secondGensPart);
                }

                firstChild.Gens.AddRange(firstGensPart);
                secondChild.Gens.AddRange(secondGensPart);
            }

            return new List<IChromosome<Neuron>>
                {
                    firstChild,
                    secondChild
                };
        }

        private void crossoverNeuroChromosome_(List<IGen<Neuron>> firstGensPart, List<IGen<Neuron>> secondGensPart)
        {
            var count = firstGensPart.Count;
            if(ShuffleWeights)
            {
                for (int i = 0; i < count; i++)
                {
                    var firstNeuroGen = firstGensPart[i] as NeuroGen;
                    var secondNeuroGen = secondGensPart[i] as NeuroGen;

                    if(ShuffleActivationFunctions)
                    {
                        if(Random.Next(0, 100) > 50)
                        {
                            var func = firstNeuroGen.Value.ActivationFunc;
                            firstNeuroGen.Value.ActivationFunc = secondNeuroGen.Value.ActivationFunc;
                            secondNeuroGen.Value.ActivationFunc = func;
                        }
                    }

                    var half = firstNeuroGen.Weights.Count / 2;

                    var first = firstNeuroGen.Weights.Take(half).ToList();
                    first.AddRange(secondNeuroGen.Weights.Skip(half).Take(count - half + 1));
                    var second = secondNeuroGen.Weights.Take(half).ToList();
                    second.AddRange(firstNeuroGen.Weights.Skip(half).Take(count - half + 1));
                    
                    firstNeuroGen.Weights = first;
                    secondNeuroGen.Weights = second;
                }
            }
            
        }
    }
}