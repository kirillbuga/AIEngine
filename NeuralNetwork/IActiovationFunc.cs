namespace NeuralNetworkCore
{
    public interface IActivationFunc
    {
        int Index { get;}
        double Activate(double s);
    }
}