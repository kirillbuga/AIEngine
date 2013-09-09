using System.Collections.Generic;
using AIEngine.GeneticAlgorithm;
using GeneticAlgorithm.Interfaces;
using NUnit.Framework;
using FluentAssertions;

namespace AIEngine.Tests
{
    [TestFixture]
    public class FitnessFunctionTest
    {
        [Test]
        [TestCaseSource("Should_ProperlyCalculateFittnessFunction_WhenIsCalled_Source")]
        public void Should_ProperlyCalculateFittnessFunction_WhenIsCalled(Chromosome chromosome, double expectedResult)
        {
            var fittnessFunction = new FittnessFunction();

            var result = fittnessFunction.Calculate(chromosome);

            result.Should().Be(expectedResult);
        }

        public IEnumerable<TestCaseData> Should_ProperlyCalculateFittnessFunction_WhenIsCalled_Source()
        {
            yield return new TestCaseData(
                new Chromosome
                    {
                        Gens = new List<IGen<int>>
                            {
                                new Gen(0),
                                new Gen(7),
                                new Gen(1),
                                new Gen(5),
                                new Gen(3),
                                new Gen(4),
                                new Gen(6),
                                new Gen(2)
                            }
                    }, 100d);            
            yield return new TestCaseData(
                new Chromosome
                    {
                        Gens = new List<IGen<int>>
                            {
                                new Gen(0),
                                new Gen(3),
                                new Gen(5),
                                new Gen(2),
                                new Gen(1),
                                new Gen(4),
                                new Gen(6),
                                new Gen(7)
                            }
                    }, 90d);            
            
            yield return new TestCaseData(
                new Chromosome
                    {
                        Gens = new List<IGen<int>>
                            {
                                new Gen(0),
                                new Gen(4),
                                new Gen(2),
                                new Gen(1),
                                new Gen(5),
                                new Gen(3),
                                new Gen(7),
                                new Gen(6)
                            }
                    }, 90d);
        }
    }
}