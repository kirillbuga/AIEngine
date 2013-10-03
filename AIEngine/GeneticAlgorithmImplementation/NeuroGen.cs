using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class NeuroGen : IGen<Neuron>
    {
        public NeuroGen()
        {
            Weights = new List<double>();
        }

        public List<double> Weights { get; set; }

        public Neuron Value { get; set; }

        public override string ToString()
        {
            return string.Format("Value: {0}, Weights Count: {1}", Value, Weights.Count);
        }
    }
}