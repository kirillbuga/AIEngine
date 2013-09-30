using GeneticAlgorithm.Interfaces;

namespace GeneticAlgorithm.Implementation.Common
{
    public class IntGen : IGen<int>
    {
        public IntGen(int value)
        {
            Value = value;
        }

        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}