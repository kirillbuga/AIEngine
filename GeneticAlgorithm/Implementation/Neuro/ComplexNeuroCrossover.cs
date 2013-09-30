using System.Collections.Generic;
using System.Linq;
using Common.Entities.Exceptions;
using GeneticAlgorithm.Interfaces;
using NeuralNetwork;

namespace GeneticAlgorithm.Implementation.Neuro
{
    public class ComplexNeuroCrossover : ICrossover<Neuron>
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

            firstChild.Gens = crossoverNeuroChromosome_(firstChrom.Gens.Take(half), secondChrom.Gens.Skip(half).Take(half));
            secondChild.Gens = crossoverNeuroChromosome_(secondChrom.Gens.Take(half), firstChrom.Gens.Skip(half).Take(half));

            return new List<IChromosome<Neuron>>
                {
                    firstChild,
                    secondChild
                };
        }

        private List<IGen<Neuron>> crossoverNeuroChromosome_(IEnumerable<IGen<Neuron>> firstGensPart, IEnumerable<IGen<Neuron>> secondGensPart)
        {
            return null;
        }
    }
}