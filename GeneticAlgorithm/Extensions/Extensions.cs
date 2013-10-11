using System.Collections.Generic;

namespace GeneticAlgorithm.Extensions
{
    public static class Extensions
    {
        public static void Swap<T>(
            this IList<T> list,
            int firstIndex,
            int secondIndex
            )
        {
            if (firstIndex == secondIndex)
            {
                return;
            }

            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }

    }
}