using System;
using AIEngine.GeneticAlgorithm;
using FizzWare.NBuilder;
using GeneticAlgorithm;
using GeneticAlgorithm.Interfaces;
using System.Linq;

namespace AIEngine
{
    class Program
    {
        public static GeneticAlgorithmCore<int> GeneticAlgorithm { get; set; }
        public const int PopulationCount = 300;
        public const int GensCount = 8;

        public Program()
        {
            IFittnessFunction<int> fittnessFunction = new FittnessFunction();
            ICrossover<int> crossover = new Crossover();
            ISelection<int> selection = new Selection();
            IMutation<int> mutation = new Mutation(2, 10);
            ITerminate<int> terminate = new Terminate();
            
            GeneticAlgorithm = new GeneticAlgorithmCore<int>(fittnessFunction, selection, crossover, mutation, terminate);
            var generator = new UniqueRandomGenerator();

            for (var i = 0; i < PopulationCount; i++)
            {
                var chromosome = new Chromosome();
                for (int j = 0; j < GensCount; j++)
                {
                    chromosome.Gens.Add(new Gen(generator.Next(0, 8)));
                }
                GeneticAlgorithm.Population.Add(chromosome);
                generator.Reset();
            }
        }

        static void Main(string[] args)
        {
            var s = new Program();

            GeneticAlgorithm.StartIterations(() =>
                {
                    Console.WriteLine(GeneticAlgorithm.Population.FirstOrDefault(x => x.FittnessValue == GeneticAlgorithm.Population.Max(y => y.FittnessValue)));
                });

            Console.Read();
        }
    }
}
