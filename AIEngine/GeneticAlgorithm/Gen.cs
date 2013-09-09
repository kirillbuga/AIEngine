using GeneticAlgorithm.Interfaces;

namespace AIEngine.GeneticAlgorithm
{
    public class Gen : IGen<int>
    {
        public Gen(int value)
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