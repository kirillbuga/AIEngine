using System.Collections.Generic;
using System.Linq;
using AIEngine.GeneticAlgorithm;
using FizzWare.NBuilder;
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
            var crossover = new Crossover();

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
                        new Chromosome
                            {
                                Gens = new List<IGen<int>>
                                    {
                                        new Gen(1),
                                        new Gen(2),
                                        new Gen(3),
                                        new Gen(4),
                                        new Gen(5),
                                        new Gen(6),
                                        new Gen(7),
                                        new Gen(8)
                                    }
                            },
                        new Chromosome
                            {
                                Gens = new List<IGen<int>>
                                    {
                                        new Gen(9),
                                        new Gen(10),
                                        new Gen(11),
                                        new Gen(12),
                                        new Gen(13),
                                        new Gen(14),
                                        new Gen(15),
                                        new Gen(16)
                                    }
                            }
                    }, new List<IChromosome<int>> // output
                        {
                            new Chromosome
                                {
                                    Gens = new List<IGen<int>>
                                        {
                                            new Gen(1),
                                            new Gen(2),
                                            new Gen(3),
                                            new Gen(4),
                                            new Gen(13),
                                            new Gen(14),
                                            new Gen(15),
                                            new Gen(16)
                                        }
                                },
                            new Chromosome
                                {
                                    Gens = new List<IGen<int>>
                                        {
                                            new Gen(9),
                                            new Gen(10),
                                            new Gen(11),
                                            new Gen(12),
                                            new Gen(5),
                                            new Gen(6),
                                            new Gen(7),
                                            new Gen(8)
                                        }
                                }
                        });
        }
    }
}
