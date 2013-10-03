using System.Collections.Generic;
using System.Linq;
using AIEngine.GeneticAlgorithmImplementation;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine
{
    public static class NeuralNetworkExtensions
    {
        public static NeuroChromosome GetNetworkState(this NeuralNetwork neuralNetwork)
        {
            var result = new List<IGen<Neuron>>();
            var neurons = neuralNetwork.Layers.SelectMany(x => x.Neurons.Select(y => y)).ToList();

            foreach (var neuron in neurons)
            {
                var gen = new NeuroGen
                    {
                        Value = neuron,
                        Weights =
                            neuron.Parent.Links.Where(x => x.Target.Index == neuron.Index)
                                  .Select(x => x.Weigth)
                                  .ToList()
                    };
                result.Add(gen);
            }

            var neuroChromosome = new NeuroChromosome {Gens = result, FittnessValue = 0};

            return neuroChromosome;
        }

        public static void SetNetworkState(this NeuralNetwork neuralNetwork, NeuroChromosome chromosome)
        {
            foreach (var layer in neuralNetwork.Layers)
            {
                var gensForCurrentLayer = chromosome.Gens.Where(x => x.Value.Parent.Index == layer.Index);
                layer.Neurons = gensForCurrentLayer.Select(x => x.Value).ToList();

                foreach (var neuronLinks in layer.Neurons.Select(localNeuron => layer.Links.Where(x => x.Target.Index == localNeuron.Index).ToList()))
                {
                    for (int j = 0; j < neuronLinks.Count; j++)
                    {
                        var gen = gensForCurrentLayer as NeuroGen;
                        neuronLinks[j].Weigth = gen.Weights[j];
                    }
                }
            }
        }
    }
}