using System;
using System.Linq;
using GeneticAlgorithm.Interfaces;
using NeuralNetworkCore;

namespace AIEngine.GeneticAlgorithmImplementation
{
    public class NeuroMutation : IMutation<Neuron>
    {
        public int MutatedGen { get; set; }
        public int MutatedPropability { get; set; }

        private Random Random { get; set; }

        public NeuroMutation(int mutatedGen, int mutatedPropability)
        {
            MutatedGen = mutatedGen;
            MutatedPropability = mutatedPropability;
            Random = new Random(DateTime.Now.Millisecond);
        }

        public void Mutate(IChromosome<Neuron> chromosome)
        {
            if (Random.Next(0, 100) > MutatedPropability)
            {
                for (int i = 0; i < MutatedGen; i++)
                {
                    var first = Random.Next(chromosome.Gens.Count);
                    var second = Random.Next(chromosome.Gens.Count);

                    mutateGen_(chromosome.Gens[first]);
                    mutateGen_(chromosome.Gens[second]);
                }
            }
        }

        private void mutateGen_(IGen<Neuron> gen)
        {
            var neuroGen = gen as AIEngine.GeneticAlgorithmImplementation.NeuroGen;
            var shouldMutateActivationFunc = Random.Next(0, 100) >= MutatedPropability;
            
            if (shouldMutateActivationFunc)
            {
                var index = Random.Next(1, ActivationFunctions.AvailableActivatonFunctions.Count() + 1);
                neuroGen.Value.ActivationFunc = ActivationFunctions.AvailableActivatonFunctions.FirstOrDefault(x => x.Index == index);
            }

            var mutatedWeightsCount = Random.Next(0, neuroGen.Weights.Count/3);

            for (int i = 0; i < mutatedWeightsCount; i++)
            {
                var nextGen = Random.Next(0, mutatedWeightsCount);
                neuroGen.Weights[nextGen] = Random.NextDouble();
            }
        }
    }
}