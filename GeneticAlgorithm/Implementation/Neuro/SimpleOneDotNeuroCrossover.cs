using System.Collections.Generic;
using Common.Entities.Exceptions;
using GeneticAlgorithm.Interfaces;
using NeuralNetwork;
using System.Linq;

namespace GeneticAlgorithm.Implementation.Neuro
{
    public class SimpleOneDotNeuroCrossover : ICrossover<Neuron>
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

            var half = firstChrom.Gens.Count / 2;

            firstChild.Gens
                      .AddRange(firstChrom.Gens.Take(half));

            firstChild.Gens
                      .AddRange(secondChrom.Gens.Skip(half).Take(half));

            secondChild.Gens
                       .AddRange(secondChrom.Gens.Take(half));

            secondChild.Gens
                      .AddRange(firstChrom.Gens.Skip(half).Take(half));

            return new List<IChromosome<Neuron>>
                {
                    firstChild,
                    secondChild
                };
        }
    }
}