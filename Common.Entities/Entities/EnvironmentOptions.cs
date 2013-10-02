using NeuralNetworkCore;

namespace Common.Entities.Entities
{
    public class EnvironmentOptions
    {
        public int AgentsCount { get; set; }
        public int FoodCount { get; set; }
        public int FieldWidth { get; set; }
        public int FieldHeight { get; set; }
        public NeuralNetwork NeuralNetwork { get; set; }

        public EnvironmentOptions()
        {
            AgentsCount = 10;
            FoodCount = 10;
            FieldHeight = 400;
            FieldWidth = 600;
        }
    }
}