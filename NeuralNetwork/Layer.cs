using System.Collections.Generic;
using System.Linq;
using Common.Entities;

namespace NeuralNetwork
{
    public class Layer
    {
        public int Index { get; set; }
        public List<Neuron> Neurons { get; set; }
        public List<Link> Links { get; set; }
        public List<double> Inputs { get; set; }

        public Layer PreviousLayer { get; set; }

        public Layer(int index, Layer previous)
        {
            Index = index;
            PreviousLayer = previous;
            Inputs = new List<double>();
            Neurons = new List<Neuron>();
            Links = new List<Link>();
        }

        public void AddNeuron(Neuron neuron)
        {
            neuron.Index = Neurons.Count;
            neuron.Parent = this;
            Neurons.Add(neuron);

            Logger.WriteAdd(neuron);
        }

        public void AddLink(Link link)
        {
            Links.Add(link);

            Logger.WriteAdd(link);
        }

        public List<double> GetOutputs()
        {
            return Neurons.Select(x => x.Output).ToList();
        }

        public void SetInputs(List<double> inputs)
        {
            Inputs = inputs;
        }

        public void Activate()
        {
            for (int i = 0; i < Neurons.Count; i++)
            {
                Neurons[i].Input = 0;
                for (int j = 0; j < PreviousLayer.Neurons.Count; j++)
                {
                    var weigth = Links.FirstOrDefault(x => x.Source.Index == j && x.Target.Index == i);
                    Neurons[i].Input += Inputs[j]*weigth.Weigth;
                }
            }

            foreach (var neuron in Neurons)
            {
                neuron.Activate();
            }
        }

        public override string ToString()
        {
            return string.Format("LAYER Index: {0}, Neurons: {1}, Links: {2}", Index, Neurons.Count, Links.Count);
        }
    }
}