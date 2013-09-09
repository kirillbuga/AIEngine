using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using System.Linq;

namespace AIEngine.GeneticAlgorithm
{
    public class Terminate : ITerminate<int>
    {
        public bool IsConditionMet(List<IChromosome<int>> population)
        {
            return population.Max(x => x.FittnessValue) == int.MaxValue;
        }
    }
}