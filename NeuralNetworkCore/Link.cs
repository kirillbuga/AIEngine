using Common.Entities;

namespace NeuralNetworkCore
{
    public class Link
    {
        private double _weigth;
        public Neuron Source { get; set; }

        public Neuron Target { get; set; }

        public double Weigth
        {
            get { return _weigth; }
            set
            {
                _weigth = value;
                Logger.WriteChange(this);
            }
        }

        public override string ToString()
        {
            return string.Format("LINK From: {0}, To: {1}, Value: {2}", Source.Index, Target.Index, Weigth);
        }
    }
}