namespace GeneticAlgorithm.Interfaces
{
    public interface IFittnessFunction<T>
    {
        double Calculate(IChromosome<T> chromosome);
    }
}