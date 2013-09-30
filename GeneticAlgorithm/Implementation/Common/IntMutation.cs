using System;
using Common.Entities.Extensions;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementation.Common
{
    public class IntMutation : IMutation<int>
    {
        public int MutatedGen { get; set; }
        public int MutatedPropability { get; set; }

        public IntMutation(int mutatedGen, int mutatedPropability)
        {
            MutatedGen = mutatedGen;
            MutatedPropability = mutatedPropability;
        }

        public void Mutate(IChromosome<int> chromosome)
        {
            var random = new Random(DateTime.Now.Millisecond);

            if (random.Next(0, 100) > MutatedPropability)
            {
                for (int i = 0; i < MutatedGen; i++)
                {
                    var first = random.Next(chromosome.Gens.Count);
                    var second = random.Next(chromosome.Gens.Count);

                    chromosome.Gens.Swap(first, second);
                }
            }
        }
    }
}