using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class NeuroFittnessFunction : IFittnessFunction<Neuron>
    {
        public double Calculate(IChromosome<Neuron> chromosome)
        {
            return 0;
        }
    }
}