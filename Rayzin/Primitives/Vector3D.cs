using System;

namespace Rayzin.Primitives
{
    public struct Vector3D : IEquatable<Vector3D>
    {
        public struct Presets
        {
            public static readonly Vector3D Zero = new Vector3D(0, 0, 0);
            public static readonly Vector3D UnitX = new Vector3D(1, 0, 0);
            public static readonly Vector3D UnitY = new Vector3D(0, 1, 0);
            public static readonly Vector3D UnitZ = new Vector3D(0, 0, 1);
        }

        public Vector3D(double x, double y, double z) => (X, Y, Z) = (x, y, z);
        public void Deconstruct(out double x, out double y, out double z) => (x, y, z) = (X, Y, Z);

        public double X { get; }
        public double Y { get; }
        public double Z { get; }

        public double W => 0;

        public bool Equals(Vector3D other) => Epsilon.Equals(X, other.X) && Epsilon.Equals(Y, other.Y) && Epsilon.Equals(Z, other.Z);

        public override bool Equals(object obj) => obj is Vector3D other && Equals(other);

        public override int GetHashCode()
        {
            throw new NotSupportedException();
        }

        public static bool operator ==(Vector3D left, Vector3D right) => left.Equals(right);

        public static bool operator !=(Vector3D left, Vector3D right) => !left.Equals(right);
        
        public override string ToString() => $"Vector3D ({X}, {Y}, {Z})";

        public static Vector3D operator +(Vector3D v1, Vector3D v2) => new Vector3D(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector3D operator -(Vector3D v1, Vector3D v2) => new Vector3D(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static Vector3D operator -(Vector3D v) => new Vector3D(-v.X, -v.Y, -v.Z);

        public static Vector3D operator *(Vector3D v, double scalar) => new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
        public static Vector3D operator *(double scalar, Vector3D v) => new Vector3D(v.X * scalar, v.Y * scalar, v.Z * scalar);
        
        public static Vector3D operator /(Vector3D v, double scalar) => new Vector3D(v.X / scalar, v.Y / scalar, v.Z / scalar);

        public double Magnitude => Math.Sqrt(X * X + Y * Y + Z * Z);

        public Vector3D Normalize()
        {
            var magnitude = Magnitude;
            if (Epsilon.Equals(magnitude, 1))
                return this;
            
            return new Vector3D(X / magnitude, Y / magnitude, Z / magnitude);
        }

        public double Dot(Vector3D v) => X * v.X + Y * v.Y + Z * v.Z;
        public Vector3D Cross(Vector3D v) => new Vector3D(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
    }
}