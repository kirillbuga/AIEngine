namespace GeneticAlgorithm.Interfaces
{
    public interface IMutation<T>
    {
        int MutatedGen { get; set; }
        int MutatedPropability { get; set; }
        void Mutate(IChromosome<T> chromosome);
    }
}