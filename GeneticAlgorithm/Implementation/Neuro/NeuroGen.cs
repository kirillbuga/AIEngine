using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using NeuralNetwork;
using System.Linq;

namespace GeneticAlgorithm.Implementation.Neuro
{
    public class NeuroGen : IGen<Neuron>
    {
        public List<double> Weights
        {
            get { return Value.Parent.Links.Where(x => x.Target.Index == Value.Index).Select(x => x.Weigth).ToList(); }
        }

        public Neuron Value { get; set; }
    }
}