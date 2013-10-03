using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class NeuroTerminate : ITerminate<Neuron>
    {
        public bool IsConditionMet(List<IChromosome<Neuron>> population)
        {
            return true;
        }
    }
}