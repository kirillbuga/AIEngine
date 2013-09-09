using System;
using System.Collections.Generic;
using System.Text;
using GeneticAlgorithm.Interfaces;

namespace AIEngine.GeneticAlgorithm
{
    public class Chromosome : IChromosome<int>
    {
        public Chromosome()
        {
            Gens = new List<IGen<int>>();
            FittnessValue = 0;
        }

        public List<IGen<int>> Gens { get; set; }
        public double FittnessValue { get; set; }

        public override string ToString()
        {
            var outStr = new StringBuilder();

            for (int i = 0; i < Gens.Count; i++)
            {
                foreach (IGen<int> gen in Gens)
                {
                    if (i == gen.Value)
                    {
                        outStr.AppendFormat("|{0}|", gen.Value);
                    }
                    else
                    {
                        outStr.Append("|_|");
                    }
                }
                outStr.Append(Environment.NewLine);
            }
            return outStr.ToString();
        }
    }
}
