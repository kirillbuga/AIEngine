using System.Collections.Generic;

namespace GeneticAlgorithm.Interfaces
{
    public interface ITerminate<T>
    {
        bool IsConditionMet(List<IChromosome<T>> population); 
    }
}