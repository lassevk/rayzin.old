using System;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    [PublicAPI]
    public struct Tuple2 : IEquatable<Tuple2>
    {
        public Tuple2(double t0, double t1) => (T0, T1) = (t0, t1);

        public readonly double T0, T1;

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

                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "index must be in the range 0..1");
                }
            }
        }

        public override bool Equals(object obj) => obj is Tuple2 other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(Tuple2 left, Tuple2 right) => left.Equals(right);

        public static bool operator !=(Tuple2 left, Tuple2 right) => !left.Equals(right);
        
        public bool Equals(Tuple2 other) => Epsilon.Equals(T0, other.T0) && Epsilon.Equals(T1, other.T1);

        public static Tuple2 operator +(Tuple2 a, Tuple2 b) => new Tuple2(a.T0 + b.T0, a.T1 + b.T1);
        public static Tuple2 operator -(Tuple2 a, Tuple2 b) => new Tuple2(a.T0 - b.T0, a.T1 - b.T1);
        public static Tuple2 operator -(Tuple2 t) => new Tuple2(-t.T0, -t.T1);
        public static Tuple2 operator *(Tuple2 t, double scalar) => new Tuple2(t.T0 * scalar, t.T1 * scalar);
        public static double operator *(Tuple2 a, Tuple2 b) => a.T0 * b.T0 + a.T1 * b.T1;
        public static Tuple2 operator /(Tuple2 t, double scalar) => new Tuple2(t.T0 / scalar, t.T1 / scalar);
    }
}