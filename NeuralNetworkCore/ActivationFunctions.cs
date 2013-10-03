using System;
using System.Collections.Generic;

namespace NeuralNetworkCore
{
    public static class ActivationFunctions
    {
        public const double Alpha = 2;

        public static IEnumerable<IActivationFunc> AvailableActivatonFunctions
        {
            get
            {
                yield return new LinearFunc {Index = 1};
                yield return new SinFunc { Index = 2 };
                yield return new TanFunc { Index = 3 };
                yield return new SigmaFunc { Index = 4 };
                yield return new RadialBasisFunc { Index = 5 };
            }
        }
    }

    public class LinearFunc : IActivationFunc
    {
        public int Index { get; set; }

        public double Activate(double s)
        {
            return 2*s;
        }
    }

    public class SinFunc : IActivationFunc
    {
        public int Index { get; set; }

        public double Activate(double s)
        {
            return Math.Sin(s);
        }
    }

    public class TanFunc : IActivationFunc
    {
        public int Index { get; set; }

        public double Activate(double s)
        {
            return  Math.Exp(-1 * Math.Pow(s, 2));
        }
    }

    public class EndLayerFunc : IActivationFunc
    {
        public int Index { get; set; }

        public double Activate(double s)
        {
            return s;
        }
    }

    public class SigmaFunc : IActivationFunc
    {
        public int Index { get; set; }

        public double Activate(double s)
        {
            return (1 / (1 + Math.Exp(-1 * ActivationFunctions.Alpha * s)));
        }
    }

    public class RadialBasisFunc : IActivationFunc
    {
        public int Index { get; set; }

        public double Activate(double s)
        {
            return Math.Tanh(ActivationFunctions.Alpha * s);
        }
    }
}