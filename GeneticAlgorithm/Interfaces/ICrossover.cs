using System.Collections.Generic;

namespace GeneticAlgorithm.Interfaces
{
    public interface ICrossover<T>
    {
        List<IChromosome<T>> Perform(List<IChromosome<T>> population);
    }
}