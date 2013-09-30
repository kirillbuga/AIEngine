using System;
using System.Linq;
using Common.Entities;

namespace NeuralNetwork
{
    public class Neuron
    {
        private static Random _random;

        private double _input;
        private double _output;
        private double _error;

        public Neuron(IActivationFunc activationFunc)
        {
            ActivationFunc = activationFunc;
            initializeRandom_();
        }

        public Neuron()
        {
            initializeRandom_();
            initializeActiovationFunction_();
        }

        public IActivationFunc ActivationFunc { get; set; }

        // input S (already summed weights with X)
        public double Input
        {
            get { return _input; }
            set
            {
                _input = value;
                Logger.WriteChange(this);
            }
        }

        // output Y
        public double Output
        {
            get { return _output; }
            set
            {
                _output = value;
                Logger.WriteChange(this);
            }
        }

        public int Index { get; set; }

        public Layer Parent { get; set; }

        // the difference beeween the expected result
        public double Error
        {
            get { return _error; }
            set
            {
                _error = value;
                Logger.WriteChange(this);
            }
        }

        private void initializeActiovationFunction_()
        {
            var index = _random.Next(1, ActivationFunctions.AvailableActivatonFunctions.Count() + 1);

            ActivationFunc =  ActivationFunctions.AvailableActivatonFunctions.FirstOrDefault(x => x.Index == index);
        }

        private void initializeRandom_()
        {
            if (_random == null)
            {
                _random = new Random((int)DateTime.Now.Ticks);
            }
        }

        public void SetInput(double input)
        {
            Input = input;
        }

        public void Activate()
        {
            Output = ActivationFunc.Activate(Input);
        }

        public override string ToString()
        {
            return string.Format("NEURON Index: {0}, Input: {1}, Output: {2}, Error: {3}", Index, Input, Output, Error);
        }

        public void Reset()
        {
            Output = 0d;
            Input = 0d;
            Error = 0d;
        }
    }
}
