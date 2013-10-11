using NUnit.Framework;
using NeuralNetworkCore;
using FluentAssertions;

namespace AIEngine.Tests
{
    [TestFixture]
    public class EngineTests
    {
        [Test]
        public void Should_CorrectSetTheNetworkState_WhenIsCalled()
        {
            var neuralNetwork =
                new NeuralNetwork()
                    .WithInputs(5)
                    .WithLayerWithSeparateActivationFunction(5)
                    .WithLayerWithSeparateActivationFunction(5)
                    .WithOutputs(1);

            var neuralNetworkState = neuralNetwork.GetNetworkState();
            var newNetwork = new NeuralNetwork();
            newNetwork.SetNetworkState(neuralNetworkState);

            neuralNetwork.Layers.Count.Should().Be(newNetwork.Layers.Count);
            neuralNetwork.Inputs.ShouldBeEquivalentTo(newNetwork.Inputs);
            neuralNetwork.Outputs.ShouldBeEquivalentTo(newNetwork.Outputs);

            for (int i = 0; i < neuralNetwork.Layers.Count; i++)
            {
                var layer = neuralNetwork.Layers[i];
                var layer2 = newNetwork.Layers[i];

                layer.Index.ShouldBeEquivalentTo(layer2.Index);
                layer.Neurons.Count.ShouldBeEquivalentTo(layer2.Neurons.Count);
                layer.Links.Count.ShouldBeEquivalentTo(layer2.Links.Count);

                for (int j = 0; j < layer.Neurons.Count; j++)
                {
                    layer.Neurons[j].Index.ShouldBeEquivalentTo(layer2.Neurons[j].Index);
                    layer.Neurons[j].ActivationFunc.ShouldBeEquivalentTo(layer2.Neurons[j].ActivationFunc);
                }

                for (int j = 0; j < layer.Links.Count; j++)
                {
                    layer.Links[j].Source.Index.ShouldBeEquivalentTo(layer2.Links[j].Source.Index);
                    layer.Links[j].Target.Index.ShouldBeEquivalentTo(layer2.Links[j].Target.Index);
                    layer.Links[j].Weigth.ShouldBeEquivalentTo(layer2.Links[j].Weigth);
                }
            }
        }
    }
}