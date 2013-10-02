using System;
using System.Collections.Generic;
using System.Linq;
using NeuralNetworkCore.GeneticAlgorithmImplementation;

//using Common.Entities;

namespace NeuralNetworkCore
{
    public class NeuralNetwork
    {
        private const double LearnParam = 0.1;
        public List<Layer> Layers { get; set; }
        public int Inputs { get; set; }
        public int Outputs { get; set; }

        private int _lastLayerNumber;
        private Random Random { get; set; }

        public NeuralNetwork()
        {
            Random = new Random((int) DateTime.Now.Ticks);
            Layers = new List<Layer>();

            //Logger.ClearLog();
            //Logger.WriteFile(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            //Logger.WriteFile("NN is initialized");
        }

        public NeuralNetwork WithInputs(int inputs)
        {
            Inputs = inputs;
            addInputLayer_();
            return this;
        }

        public NeuralNetwork WithOutputs(int outputs)
        {
            Outputs = outputs;
            addOutputLayer_();
            return this;
        }

        public NeuralNetwork WithLayerWithCommonActivationFunction(int neuronCount, IActivationFunc activationFunc)
        {
            var layer = createNewLayer_();

            for (int i = 0; i < neuronCount; i++)
            {
                var neuron = new Neuron(activationFunc);
                layer.AddNeuron(neuron);
            }

            initializeLinks_(layer);
            addLayer_(layer);

            return this;
        }

        public NeuralNetwork WithLayerWithSeparateActivationFunction(int neuronCount)
        {
            var layer = createNewLayer_();

            for (int i = 0; i < neuronCount; i++)
            {
                var neuron = new Neuron();
                layer.AddNeuron(neuron);
            }

            initializeLinks_(layer);
            addLayer_(layer);

            return this;
        }

        public List<double> Activate(List<double> input)
        {
            var inputs = activateLayer_(1, input);

            for (int i = 2; i < Layers.Count; i++)
            {
                inputs = activateLayer_(i, inputs);
            }

            return getLayerByNumber_(_lastLayerNumber).GetOutputs();
        }

        public void Correct(List<double> expected)
        {
            // calculate error on last layer
            var neurons = getLayerByNumber_(_lastLayerNumber).Neurons;

            for (int i = 0; i < neurons.Count; i++)
            {
                neurons[i].Error = neurons[i].Output*(1 - neurons[i].Output)*(expected[i] - neurons[i].Output);
            }

            // calculate error on all layers
            for (int k = _lastLayerNumber; k > 1; k--)
            {
                var nextLayer = getLayerByNumber_(k);
                var currentLayer = nextLayer.PreviousLayer;

                for (int i = 0; i < currentLayer.Neurons.Count; i++)
                {
                    var errorSumm = 0d;
                    for (int j = 0; j < nextLayer.Neurons.Count; j++)
                    {
                        var link = nextLayer.Links.FirstOrDefault(x => x.Source.Index == i && x.Target.Index == j);
                        errorSumm += nextLayer.Neurons[j].Error*link.Weigth;
                    }

                    var error = currentLayer.Neurons[i].Output * (1 - currentLayer.Neurons[i].Output) * errorSumm;

                    if (double.IsNaN(error) || Double.IsInfinity(error))
                    {
                        throw new Exception("NAN or Infinity during errors calculation.");
                    }

                    currentLayer.Neurons[i].Error = error;
                }

            }

            // correct weights
            for (int k = _lastLayerNumber; k > 0; k--)
            {
                var currentLayer = getLayerByNumber_(k);
                var previousLayer = currentLayer.PreviousLayer;

                for (int i = 0; i < previousLayer.Neurons.Count; i++)
                {
                    for (int j = 0; j < currentLayer.Neurons.Count; j++)
                    {
                        var link = currentLayer.Links.FirstOrDefault(x => x.Source.Index == i && x.Target.Index == j);
                        var output = previousLayer.Index == 0 ? currentLayer.Inputs[i] : previousLayer.Neurons[i].Output;
                        var weigth = output*currentLayer.Neurons[j].Error*LearnParam;
                        if (double.IsNaN(weigth) || Double.IsInfinity(weigth))
                        {
                            throw new Exception("NAN or Infinity during correcting wights.");
                        }
                        link.Weigth += weigth;
                    }
                }
            }
        }

        public void Reset()
        {
            foreach (var layer in Layers)
            {
                foreach (var neuron in layer.Neurons)
                {
                    neuron.Reset();
                }
            }
        }

        public List<Neuron> GetNetworkState()
        {
            return Layers.SelectMany(x => x.Neurons.Select(y => y)).ToList();
        }

        public void SetNetworkState(NeuroChromosome chromosome)
        {
            foreach (var layer in Layers)
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

        public string NetworkStateLog()
        {
            var result = "";

            foreach (var layer in Layers)
            {
                result += layer + Environment.NewLine;
                result = layer.Neurons.Aggregate(result, (current, neuron) => current + (neuron + Environment.NewLine));
                result = layer.Links.Aggregate(result, (current, link) => current + (link + Environment.NewLine));
            }

            return result;
        }

        public double GetSummaryError()
        {
            return Layers.Sum(layer => layer.Neurons.Sum(neuron => neuron.Error));
        }

        #region Private members

        private List<double> activateLayer_(int layerNumber, List<double> inputs)
        {
            var layer = getLayerByNumber_(layerNumber);
            layer.SetInputs(inputs);
            layer.Activate();
            inputs = layer.GetOutputs();
            return inputs;
        }

        private Layer getLayerByNumber_(int index)
        {
            return Layers.FirstOrDefault(x => x.Index == index);
        }

        private void addOutputLayer_()
        {
            var layer = createNewLayer_();

            for (int i = 0; i < Outputs; i++)
            {
                var neuron = new Neuron {ActivationFunc = new EndLayerFunt()};
                layer.AddNeuron(neuron);
            }

            initializeLinks_(layer);
            addLayer_(layer);

        }

        private void addInputLayer_()
        {
            var layer = new Layer(0, null);

            for (int i = 0; i < Inputs; i++)
            {
                var neuron = new Neuron();
                layer.AddNeuron(neuron);
            }

            addLayer_(layer);
        }

        private Layer createNewLayer_()
        {
            var previousLayerNumber = _lastLayerNumber;
            return new Layer(++_lastLayerNumber, getLayerByNumber_(previousLayerNumber));
        }

        private void addLayer_(Layer layer)
        {
            Layers.Add(layer);
            //Logger.WriteAdd(layer);
        }

        private void initializeLinks_(Layer layer)
        {
            foreach (var target in layer.Neurons)
            {
                foreach (var source in layer.PreviousLayer.Neurons)
                {
                    layer.AddLink(new Link
                    {
                        Source = source,
                        Target = target,
                        Weigth = Random.NextDouble()
                    });
                }
            }
        }

        #endregion

        #region Test tools

        public NeuralNetwork InitializeWithTestData()
        {
            foreach (var layer in Layers)
            {
                var s = 0d;
                foreach (var link in layer.Links)
                {
                    s += 0.1d;
                    link.Weigth = s;
                }

                for (int index = 0; index < layer.Neurons.Count - 1; index++)
                {
                    var neuron = layer.Neurons[index];
                    neuron.ActivationFunc = new LinearFunc();
                }
            }

            return this;
        }

        #endregion
    }
}