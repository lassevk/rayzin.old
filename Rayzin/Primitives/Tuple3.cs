using System;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    [PublicAPI]
    public struct Tuple3 : IEquatable<Tuple3>
    {
        public Tuple3(double t0, double t1, double t2) => (T0, T1, T2) = (t0, t1, t2);

        public readonly double T0, T1, T2;

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
                    
                    default:
                        throw new ArgumentOutOfRangeException(nameof(index), "index must be in the range 0..2");
                }
            }
        }

        public override bool Equals(object obj) => obj is Tuple3 other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(Tuple3 left, Tuple3 right) => left.Equals(right);

        public static bool operator !=(Tuple3 left, Tuple3 right) => !left.Equals(right);

        public bool Equals(Tuple3 other) => Epsilon.Equals(T0, other.T0) && Epsilon.Equals(T1, other.T1) && Epsilon.Equals(T2, other.T2);

        public static Tuple3 operator +(Tuple3 a, Tuple3 b) => new Tuple3(a.T0 + b.T0, a.T1 + b.T1, a.T2 + b.T2);
        public static Tuple3 operator -(Tuple3 a, Tuple3 b) => new Tuple3(a.T0 - b.T0, a.T1 - b.T1, a.T2 - b.T2);
        public static Tuple3 operator -(Tuple3 t) => new Tuple3(-t.T0, -t.T1, -t.T2);
        public static Tuple3 operator *(Tuple3 t, double scalar) => new Tuple3(t.T0 * scalar, t.T1 * scalar, t.T2 * scalar);
        public static double operator *(Tuple3 a, Tuple3 b) => a.T0 * b.T0 + a.T1 * b.T1 + a.T2 * b.T2;
        public static Tuple3 operator /(Tuple3 t, double scalar) => new Tuple3(t.T0 / scalar, t.T1 / scalar, t.T2 / scalar);
    }
}