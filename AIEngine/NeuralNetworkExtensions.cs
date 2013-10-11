using System.Linq;
using AIEngine.GeneticAlgorithmImplementation;
using NeuralNetworkCore;

namespace AIEngine
{
    public static class NeuralNetworkExtensions
    {
        public static NeuroChromosome GetNetworkState(this NeuralNetwork neuralNetwork)
        {
            var networkState =  neuralNetwork.Layers.ToList();

            var neuroChromosome = new NeuroChromosome();
            foreach (var layer in networkState)
            {
                foreach (var neuron in layer.Neurons)
                {
                    var neuroGen = new NeuroGen
                    {
                        Value = neuron,
                        Weights = layer.Links.Where(x => x.Target.Index == neuron.Index).Select(x => x.Weigth).ToList()
                    };

                    neuroChromosome.Gens.Add(neuroGen);
                }
            }

            return neuroChromosome;
        }

        public static void SetNetworkState(this NeuralNetwork neuralNetwork, NeuroChromosome chromosome)
        {
            neuralNetwork.Reset();

            var layersCount = chromosome.Gens.Select(x => x.Value.Parent.Index).Distinct().Count();
            var inputsCount = chromosome.Gens.Count(x => x.Value.Parent.Index == 0);
            var outputsCount = chromosome.Gens.Count(x => x.Value.Parent.Index == layersCount - 1);

            neuralNetwork.WithInputs(inputsCount);

            for (int i = 1; i < layersCount - 1; i++)
            {
                var neuronsOnCurrentLayer = chromosome.Gens.Count(x => x.Value.Parent.Index == i);
                neuralNetwork.WithLayerWithSeparateActivationFunction(neuronsOnCurrentLayer);
            }

            neuralNetwork.WithOutputs(outputsCount);
            
            foreach (var layer in neuralNetwork.Layers)
            {
                var gensForCurrentLayer = chromosome.Gens.Where(x => x.Value.Parent.Index == layer.Index).ToList();
                layer.Neurons = gensForCurrentLayer.Select(x => x.Value).ToList();

                foreach (var gen in gensForCurrentLayer)
                {
                    var neuroGen = gen as NeuroGen;
                    var linksInNetwork = layer.Links.Where(x => x.Target.Index == neuroGen.Value.Index).ToList();

                    for (int i = 0; i < linksInNetwork.Count(); i++)
                    {
                        linksInNetwork[i].Weigth = neuroGen.Weights[i];
                    }
                }
            }
        }
    }
}