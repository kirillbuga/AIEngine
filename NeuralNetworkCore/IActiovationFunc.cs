namespace NeuralNetworkCore
{
    public interface IActivationFunc
    {
        int Index { get; set; }
        double Activate(double s);
    }
}