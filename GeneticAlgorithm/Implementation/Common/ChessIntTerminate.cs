using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using System.Linq;

namespace GeneticAlgorithm.Implementation.Common
{
    public class ChessIntTerminate : ITerminate<int>
    {
        public bool IsConditionMet(List<IChromosome<int>> population)
        {
            return population.Max(x => x.FittnessValue) == int.MaxValue;
        }
    }
}