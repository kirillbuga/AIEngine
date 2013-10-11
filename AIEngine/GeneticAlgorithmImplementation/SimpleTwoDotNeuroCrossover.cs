using System;
using System.Collections.Generic;
using GeneticAlgorithm.Exceptions;
using GeneticAlgorithm.Interfaces;
using System.Linq;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class SimpleTwoDotNeuroCrossover : ICrossover<Neuron>
    {
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

            var count = firstChrom.Gens.Count;
            var part = count / 3;

            firstChild.Gens
                      .AddRange(firstChrom.Gens.Take(part));

            firstChild.Gens
                      .AddRange(secondChrom.Gens.Skip(part).Take(part));

            firstChild.Gens
                      .AddRange(firstChrom.Gens.Skip(part * 2).Take(count - part * 2));

            secondChild.Gens
                       .AddRange(secondChrom.Gens.Take(part));

            secondChild.Gens
                      .AddRange(firstChrom.Gens.Skip(part).Take(part));

            secondChild.Gens
                      .AddRange(secondChrom.Gens.Skip(part * 2).Take(count - part * 2));

            if (firstChild.Gens.Count != count || secondChild.Gens.Count != count)
            {
                throw new Exception("The crossover was performed wrongly");
            }

            return new List<IChromosome<Neuron>>
                {
                    firstChild,
                    secondChild
                };
        }
    }
}