using System;

namespace Rayzin.Primitives
{
    public readonly struct RzPoint : IEquatable<RzPoint>
    {
        public struct Presets
        {
            public static readonly RzPoint Zero = new RzPoint(0, 0, 0);
            public static readonly RzPoint Origin = new RzPoint(0, 0, 0);
        }

        public RzPoint(double x, double y, double z) => (X, Y, Z) = (x, y, z);
        public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public bool Equals(RzPoint other) => RzEpsilon.Equals(X, other.X) && RzEpsilon.Equals(Y, other.Y) && RzEpsilon.Equals(Z, other.Z);

        public override bool Equals(object obj) => obj is RzPoint other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzPoint left, RzPoint right) => left.Equals(right);

        public static bool operator !=(RzPoint left, RzPoint right) => !left.Equals(right);

        public override string ToString() => $"RzPoint ({X}, {Y}, {Z})";

        public static RzVector operator -(RzPoint p1, RzPoint p2) => new RzVector(p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
        public static RzPoint operator +(RzPoint p, RzVector v) => new RzPoint(p.X + v.X, p.Y + v.Y, p.Z + v.Z);

        public static implicit operator RzPoint((double x, double y, double z) tuple) => new RzPoint(tuple.x, tuple.y, tuple.z);
        public static implicit operator (double x, double y, double z)(RzPoint point) => (point.X, point.Y, point.Z);
    }
}