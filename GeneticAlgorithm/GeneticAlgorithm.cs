using System;
using System.Collections.Generic;
using System.Linq;
using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm
{
    public class GeneticAlgorithm<T>
    {
        public GeneticAlgorithm(IFittnessFunction<T> fittnessFunction, ISelection<T> selection, ICrossover<T> crossover,
                                IMutation<T> mutation, ITerminate<T> terminate)
        {
            Population = new List<IChromosome<T>>();
            FittnessFunction = fittnessFunction;
            Selection = selection;
            Crossover = crossover;
            Mutation = mutation;
            Terminate = terminate;
        }

        public List<IChromosome<T>> Population { get; set; }

        public IFittnessFunction<T> FittnessFunction { get; set; }

        public ISelection<T> Selection { get; set; }

        public ICrossover<T> Crossover { get; set; }

        public IMutation<T> Mutation { get; set; }

        public ITerminate<T> Terminate { get; set; } 

        public void PerformIteration()
        {
            foreach (var chromosome in Population)
            {
                chromosome.FittnessValue = FittnessFunction.Calculate(chromosome);
            }

            var selection = Selection.Select(Population);
            var crossover = Crossover.Perform(selection);

            foreach (var chromosome in crossover)
            {
                Mutation.Mutate(chromosome);
            }

            Population.Remove(Population.FirstOrDefault(
                x => x.FittnessValue == Population.Min(y => y.FittnessValue)));
            Population.Remove(Population.FirstOrDefault(
                x => x.FittnessValue == Population.Min(y => y.FittnessValue)));

            Population.AddRange(crossover);
        }

        public void StartIterations(Action callback)
        {
            do
            {
                PerformIteration();
            } while (!Terminate.IsConditionMet(Population));

            callback();
        }
    }
}
