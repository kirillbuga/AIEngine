using System.Collections.Generic;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;
using System.Linq;

namespace NeuralNetworkCore.GeneticAlgorithmImplementation
{
    public class NeuroGen : IGen<Neuron>
    {
        private List<double> _weights; 

        public List<double> Weights
        {
            get {
                return _weights ??
                       (_weights =
                        Value.Parent.Links.Where(x => x.Target.Index == Value.Index).Select(x => x.Weigth).ToList());
            }
            set { _weights = value; }
        }

        public Neuron Value { get; set; }
    }
}