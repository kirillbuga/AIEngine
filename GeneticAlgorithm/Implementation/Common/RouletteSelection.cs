using System;
using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using System.Linq;

namespace GeneticAlgorithm.Implementation.Common
{
    public class RouletteSelection<T> : ISelection<T>
    {
        public List<IChromosome<T>> Select(List<IChromosome<T>> population)
        {
            var random = new Random(DateTime.Now.Millisecond);

            var fitnessSumm = population.Sum(x => x.FittnessValue);

            var roulette = new List<double> {0};
            var currentValue = 0d;

            foreach (var chromosome in population)
            {
                currentValue += chromosome.FittnessValue/fitnessSumm;
                roulette.Add(currentValue);
            }

            var firstRouletteValue = random.NextDouble();
            var secondRouletteValue = random.NextDouble();

            var result = new List<IChromosome<T>>();

            for (int i = 0; i < roulette.Count - 1; i++)
            {
                if (roulette[i] < firstRouletteValue && firstRouletteValue < roulette[i + 1])
                {
                    result.Add(population[i]);
                }
                if (roulette[i] < secondRouletteValue && secondRouletteValue < roulette[i + 1])
                {
                    result.Add(population[i]);
                }
                if (result.Count == 2)
                {
                    return result;
                }
            }

            throw new ApplicationException("Something wrong during selection. Result count equal to " + result.Count);
        }
    }
}