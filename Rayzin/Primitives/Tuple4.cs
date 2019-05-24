using System;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    [PublicAPI]
    public struct Tuple4 : IEquatable<Tuple4>
    {
        public Tuple4(double t0, double t1, double t2, double t3) => (T0, T1, T2, T3) = (t0, t1, t2, t3);

        public readonly double T0, T1, T2, T3;

        public double this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return T0;

                    case 1:
                        return T1;

                    case 2:
                        return T2;

                    case 3:
                        return T3;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "index must be in the range 0..3");
                }
            }
        }

        public override bool Equals(object obj) => obj is Tuple4 other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(Tuple4 left, Tuple4 right) => left.Equals(right);

        public static bool operator !=(Tuple4 left, Tuple4 right) => !left.Equals(right);

        public bool Equals(Tuple4 other)
            => Epsilon.Equals(T0, other.T0) && Epsilon.Equals(T1, other.T1) && Epsilon.Equals(T2, other.T2) && Epsilon.Equals(T3, other.T3);

        public static Tuple4 operator +(Tuple4 a, Tuple4 b) => new Tuple4(a.T0 + b.T0, a.T1 + b.T1, a.T2 + b.T2, a.T3 + b.T3);
        public static Tuple4 operator -(Tuple4 a, Tuple4 b) => new Tuple4(a.T0 - b.T0, a.T1 - b.T1, a.T2 - b.T2, a.T3 - b.T3);
        public static Tuple4 operator -(Tuple4 t) => new Tuple4(-t.T0, -t.T1, -t.T2, -t.T3);
        public static Tuple4 operator *(Tuple4 t, double scalar) => new Tuple4(t.T0 * scalar, t.T1 * scalar, t.T2 * scalar, t.T3 * scalar);
        public static double operator *(Tuple4 a, Tuple4 b) => a.T0 * b.T0 + a.T1 * b.T1 + a.T2 * b.T2 + a.T3 * b.T3;
        public static Tuple4 operator /(Tuple4 t, double scalar) => new Tuple4(t.T0 / scalar, t.T1 / scalar, t.T2 / scalar, t.T3 / scalar);
    }
}