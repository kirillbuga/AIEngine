using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using GeneticAlgorithm.Implementation.Common;
using GeneticAlgorithm.Interfaces;
using NUnit.Framework;
using FluentAssertions;

namespace AIEngine.Tests
{
    public class CrossoverTests
    {
        public UniqueRandomGenerator Generator { get; set; }

        [Test]
        [TestCaseSource("Should_CorrectPerformOneDotCrossover_WhenIsCalled_Source")]
        public void Should_CorrectPerformOneDotCrossover_WhenIsCalled(List<IChromosome<int>> input, List<IChromosome<int>> output)
        {
            var crossover = new IntOneDotCrossover();

            var result = crossover
                .Perform(input)
                .Select(x => x.Gens)
                .ToList();

            var first = result.First();
            var second = result.Last();

            for (int i = 0; i < first.Count; i++)
            {
                first[i].Value.Should().Be(output.First().Gens[i].Value);
            }

            for (int i = 0; i < second.Count; i++)
            {
                second[i].Value.Should().Be(output.Last().Gens[i].Value);
            }
        }

        public IEnumerable<TestCaseData> Should_CorrectPerformOneDotCrossover_WhenIsCalled_Source()
        {
            yield return new TestCaseData(
                new List<IChromosome<int>> // input
                    {
                        new IntChromosome
                            {
                                Gens = new List<IGen<int>>
                                    {
                                        new IntGen(1),
                                        new IntGen(2),
                                        new IntGen(3),
                                        new IntGen(4),
                                        new IntGen(5),
                                        new IntGen(6),
                                        new IntGen(7),
                                        new IntGen(8)
                                    }
                            },
                        new IntChromosome
                            {
                                Gens = new List<IGen<int>>
                                    {
                                        new IntGen(9),
                                        new IntGen(10),
                                        new IntGen(11),
                                        new IntGen(12),
                                        new IntGen(13),
                                        new IntGen(14),
                                        new IntGen(15),
                                        new IntGen(16)
                                    }
                            }
                    }, new List<IChromosome<int>> // output
                        {
                            new IntChromosome
                                {
                                    Gens = new List<IGen<int>>
                                        {
                                            new IntGen(1),
                                            new IntGen(2),
                                            new IntGen(3),
                                            new IntGen(4),
                                            new IntGen(13),
                                            new IntGen(14),
                                            new IntGen(15),
                                            new IntGen(16)
                                        }
                                },
                            new IntChromosome
                                {
                                    Gens = new List<IGen<int>>
                                        {
                                            new IntGen(9),
                                            new IntGen(10),
                                            new IntGen(11),
                                            new IntGen(12),
                                            new IntGen(5),
                                            new IntGen(6),
                                            new IntGen(7),
                                            new IntGen(8)
                                        }
                                }
                        });
        }
    }
}
