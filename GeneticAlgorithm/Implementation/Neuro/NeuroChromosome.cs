using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using NeuralNetwork;

namespace GeneticAlgorithm.Implementation.Neuro
{
    public class NeuroChromosome : IChromosome<Neuron>
    {
        public List<IGen<Neuron>> Gens { get; set; }
        public double FittnessValue { get; set; }
    }
}