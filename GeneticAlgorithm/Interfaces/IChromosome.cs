using System.Collections.Generic;

namespace GeneticAlgorithm.Interfaces
{
    public interface IChromosome<T>
    {
        List<IGen<T>> Gens { get; set; }

        double FittnessValue { get; set; }
    }
}