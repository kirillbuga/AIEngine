using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class NeuroGen : IGen<Neuron>
    {
        public List<double> Weights { get; set; }

        public Neuron Value { get; set; }
    }
}