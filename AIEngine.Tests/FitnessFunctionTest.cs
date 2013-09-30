using System.Collections.Generic;
using FluentAssertions;
using GeneticAlgorithm.Implementation.Common;
using GeneticAlgorithm.Interfaces;
using NUnit.Framework;

namespace AIEngine.Tests
{
    [TestFixture]
    public class FitnessFunctionTest
    {
        [Test]
        [TestCaseSource("Should_ProperlyCalculateFittnessFunction_WhenIsCalled_Source")]
        public void Should_ProperlyCalculateFittnessFunction_WhenIsCalled(IntChromosome chromosome, double expectedResult)
        {
            var fittnessFunction = new ChessIntFittnessFunction();

            var result = fittnessFunction.Calculate(chromosome);

            result.Should().Be(expectedResult);
        }

        public IEnumerable<TestCaseData> Should_ProperlyCalculateFittnessFunction_WhenIsCalled_Source()
        {
            yield return new TestCaseData(
                new IntChromosome
                    {
                        Gens = new List<IGen<int>>
                            {
                                new IntGen(0),
                                new IntGen(7),
                                new IntGen(1),
                                new IntGen(5),
                                new IntGen(3),
                                new IntGen(4),
                                new IntGen(6),
                                new IntGen(2)
                            }
                    }, 100d);            
            yield return new TestCaseData(
                new IntChromosome
                    {
                        Gens = new List<IGen<int>>
                            {
                                new IntGen(0),
                                new IntGen(3),
                                new IntGen(5),
                                new IntGen(2),
                                new IntGen(1),
                                new IntGen(4),
                                new IntGen(6),
                                new IntGen(7)
                            }
                    }, 90d);            
            
            yield return new TestCaseData(
                new IntChromosome
                    {
                        Gens = new List<IGen<int>>
                            {
                                new IntGen(0),
                                new IntGen(4),
                                new IntGen(2),
                                new IntGen(1),
                                new IntGen(5),
                                new IntGen(3),
                                new IntGen(7),
                                new IntGen(6)
                            }
                    }, 90d);
        }
    }
}