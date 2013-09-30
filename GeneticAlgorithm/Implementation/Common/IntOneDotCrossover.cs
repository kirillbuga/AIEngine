using System;
using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using System.Linq;

namespace GeneticAlgorithm.Implementation.Common
{
    public class IntOneDotCrossover : ICrossover<int>
    {
        public List<IChromosome<int>> Perform(List<IChromosome<int>> population)
        {
            if (population.Count != 2)
            {
                throw new ApplicationException(
                    "You must provide 2 and only two chromosomes for propover crossover operation");
            }

            var firstChrom = population.First();
            var secondChrom = population.Last();

            var firstChild = new IntChromosome();
            var secondChild = new IntChromosome();

            var half = firstChrom.Gens.Count/2;

            firstChild.Gens
                      .AddRange(firstChrom.Gens.Take(half));

            firstChild.Gens
                      .AddRange(secondChrom.Gens.Skip(half).Take(half));

            secondChild.Gens
                       .AddRange(secondChrom.Gens.Take(half));

            secondChild.Gens
                      .AddRange(firstChrom.Gens.Skip(half).Take(half));

            return new List<IChromosome<int>>
                {
                    firstChild,
                    secondChild
                };
        }
    }
}