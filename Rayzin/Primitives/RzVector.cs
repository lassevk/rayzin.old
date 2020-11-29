using System;

namespace Rayzin.Primitives
{
    public readonly struct RzVector : IEquatable<RzVector>
    {
        public struct Presets
        {
            public static readonly RzVector Zero = (0, 0, 0);
            public static readonly RzVector UnitX = (1, 0, 0);
            public static readonly RzVector UnitY = (0, 1, 0);
            public static readonly RzVector UnitZ = (0, 0, 1);
        }

        public RzVector(double x, double y, double z) => (X, Y, Z) = (x, y, z);
        public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public bool Equals(RzVector other) => RzEpsilon.Equals(X, other.X) && RzEpsilon.Equals(Y, other.Y) && RzEpsilon.Equals(Z, other.Z);

        public override bool Equals(object obj) => obj is RzVector other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzVector left, RzVector right) => left.Equals(right);

        public static bool operator !=(RzVector left, RzVector right) => !left.Equals(right);
        
        public override string ToString() => $"RzVector ({X}, {Y}, {Z})";

        public static RzVector operator +(RzVector v1, RzVector v2) => new RzVector(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static RzVector operator -(RzVector v1, RzVector v2) => new RzVector(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static RzVector operator -(RzVector v) => new RzVector(-v.X, -v.Y, -v.Z);

        public static RzVector operator *(RzVector v, double scalar) => new RzVector(v.X * scalar, v.Y * scalar, v.Z * scalar);
        public static RzVector operator *(double scalar, RzVector v) => new RzVector(v.X * scalar, v.Y * scalar, v.Z * scalar);
        
        public static RzVector operator /(RzVector v, double scalar) => new RzVector(v.X / scalar, v.Y / scalar, v.Z / scalar);

        public double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z);

        public RzVector Normalize()
        {
            var magnitude = Magnitude;
            if (RzEpsilon.Equals(magnitude, 1))
                return this;

            if (RzEpsilon.Equals(magnitude, 0))
                throw new DivideByZeroException("Vector has a magnitude of 0, no direction known");
            
            return new RzVector(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public double Dot(RzVector v) => X * v.X + Y * v.Y + Z * v.Z;
        public RzVector Cross(RzVector v) => new RzVector(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);

        public RzVector Reflect(RzVector normalVector) => this - normalVector * 2 * this.Dot(normalVector);

        public static implicit operator RzVector((double x, double y, double z) tuple) => new RzVector(tuple.x, tuple.y, tuple.z);
        public static implicit operator (double x, double y, double z)(RzVector vector) => (vector.X, vector.Y, vector.Z);
    }
}