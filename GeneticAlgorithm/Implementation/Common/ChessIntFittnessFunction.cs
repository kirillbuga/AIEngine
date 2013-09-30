using Common.Entities.Profiler;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementation.Common
{
    public class ChessIntFittnessFunction : IFittnessFunction<int>
    {
        public double Calculate(IChromosome<int> chromosome)
        {
            var gensCount = chromosome.Gens.Count;
            var gens = chromosome.Gens;
            var result = 0d;
            for (int i = 0; i < gensCount; i++)
            {
                for (int j = i + 1; j < gensCount; j++)
                {
                    if (chromosome.Gens[i].Value == chromosome.Gens[j].Value)
                    {
                        return 0;
                    }
                }
            }
            Profiler.Start("Calculate");
            for (int k = 0; k < gensCount; k++)
            {
                var iValue = chromosome.Gens[k].Value++;
                var jValue = chromosome.Gens[k].Value--;
                for (int i = k + 1, j = k - 1; i < gensCount || j > 0; i++, j--, iValue++, jValue--)
                {
                    if (i < gensCount)
                    {
                        if (gens[i].Value == iValue)
                        {
                            result += 10;
                        }
                        if (gens[i].Value == jValue)
                        {
                            result += 10;
                        }
                    }

                    if (j > 0)
                    {
                        if (gens[j].Value == iValue)
                        {
                            result += 10;
                        }
                        if (gens[j].Value == jValue)
                        {
                            result += 10;
                        }
                    }
                }
            }
            Profiler.Stop();
            if (result == 0)
            {
                return int.MaxValue;
            }
            else
            {
                return result;
            }
        }
    }
}