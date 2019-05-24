using System;

namespace Rayzin
{
    public struct Epsilon
    {
        public const double Value = 1e-5;

        public static bool Equals(double a, double b) => Math.Abs(a - b) < Value;
    }
}