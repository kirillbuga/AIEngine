namespace Common.Entities
{
    public class Food : ICoordinated
    {
        public Food(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Food()
        {
        }

        public double X { get; set; }
        public double Y { get; set; }
    }
}