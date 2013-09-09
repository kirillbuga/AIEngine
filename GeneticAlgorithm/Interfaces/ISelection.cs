using System.Collections.Generic;

namespace GeneticAlgorithm.Interfaces
{
    public interface ISelection<T>
    {
        List<IChromosome<T>> Select(List<IChromosome<T>> population);
    }
}