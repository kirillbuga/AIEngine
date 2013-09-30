using System;
using System.Collections.Generic;

namespace NeuralNetwork
{
    public static class ActivationFunctions
    {
        public const double Alpha = 2;

        public static IEnumerable<IActivationFunc> AvailableActivatonFunctions
        {
            get
            {
                //yield return new ThresholdFunc();
                yield return new LinearFunc();
                //yield return new ALinearFunc();
                yield return new SinFunc();
                yield return new TanFunc();
                //yield return new EndLayerFunt();
                //yield return new SigmaFunc();
                yield return new RadialBasisFunc();
            }
        }
    }

    //public class ThresholdFunc : IActivationFunc
    //{
    //    public int Index
    //    {
    //        get { return 1; }
    //    }

    //    public double Activate(double s)
    //    {
    //        return s >= 0 ? 1 : 0;
    //    }
    //}

    public class LinearFunc : IActivationFunc
    {
        public int Index
        {
            get { return 1; }
        }

        public double Activate(double s)
        {
            return 2*s;
        }
    }

    //public class ALinearFunc : IActivationFunc
    //{
    //    public int Index
    //    {
    //        get { return 3; }
    //    }

    //    public double Activate(double s)
    //    {
    //        return s >= 0 ? 2d*s : 0;
    //    }
    //}

    public class SinFunc : IActivationFunc
    {
        public int Index
        {
            get { return 2; }
        }

        public double Activate(double s)
        {
            return Math.Sin(s);
        }
    }

    public class TanFunc : IActivationFunc
    {
        public int Index
        {
            get { return 3; }
        }

        public double Activate(double s)
        {
            return  Math.Exp(-1 * Math.Pow(s, 2));
        }
    }

    public class EndLayerFunt : IActivationFunc
    {
        public int Index
        {
            get { return 99; }
        }

        public double Activate(double s)
        {
            return s;
        }
    }

    //public class SigmaFunc : IActivationFunc
    //{
    //    public int Index
    //    {
    //        get { return 4; }
    //    }
    //    public double Activate(double s)
    //    {
    //        return (1 / (1 + Math.Exp(-1 * ActivationFunctions.Alpha * s)));
    //    }
    //}

    public class RadialBasisFunc : IActivationFunc
    {
        public int Index
        {
            get { return 4; }
        }
        public double Activate(double s)
        {
            return Math.Tanh(ActivationFunctions.Alpha * s);
        }
    }
}