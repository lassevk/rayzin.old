using System;

namespace Rayzin.Core
{
    public struct ColorF : IEquatable<ColorF>
    {
        public struct Presets
        {
            public static readonly ColorF Black = new ColorF(0, 0, 0);
            public static readonly ColorF Red = new ColorF(1, 0, 0);
            public static readonly ColorF Green = new ColorF(0, 1, 0);
            public static readonly ColorF Blue = new ColorF(0, 0, 1);
            public static readonly ColorF White = new ColorF(1, 1, 1);
        }

        public ColorF(double red, double green, double blue) => (Red, Green, Blue) = (red, green, blue);
        public void Deconstruct(out double red, out double green, out double blue) => (red, green, blue) = (Red, Green, Blue);

        public double Red { get; }

        public double Green { get; }

        public double Blue { get; }

        public bool Equals(ColorF other)
            => Epsilon.Equals(Red, other.Red) && Epsilon.Equals(Green, other.Green) && Epsilon.Equals(Blue, other.Blue);

        public override bool Equals(object obj) => obj is ColorF other && Equals(other);

        public override int GetHashCode()
        {
            throw new NotSupportedException();
        }

        public static bool operator ==(ColorF left, ColorF right) => left.Equals(right);

        public static bool operator !=(ColorF left, ColorF right) => !left.Equals(right);

        public static ColorF operator +(ColorF c1, ColorF c2) => new ColorF(c1.Red + c2.Red, c1.Green + c2.Green, c1.Blue + c2.Blue);
        public static ColorF operator -(ColorF c1, ColorF c2) => new ColorF(c1.Red - c2.Red, c1.Green - c2.Green, c1.Blue - c2.Blue);
        public static ColorF operator -(ColorF c) => new ColorF(-c.Red, -c.Green, -c.Blue);

        public static ColorF operator *(ColorF c, double scalar) => new ColorF(c.Red * scalar, c.Green * scalar, c.Blue * scalar);
        public static ColorF operator *(double scalar, ColorF c) => new ColorF(c.Red * scalar, c.Green * scalar, c.Blue * scalar);
        public static ColorF operator *(ColorF c1, ColorF c2) => new ColorF(c1.Red * c2.Red, c1.Green * c2.Green, c1.Blue * c2.Blue);

        public static ColorF operator /(ColorF c, double scalar) => new ColorF(c.Red / scalar, c.Green / scalar, c.Blue / scalar);

        public override string ToString() => $"ColorF ({Red}, {Green}, {Blue})";

        public double Magnitude => Math.Sqrt(Red * Red + Green * Green + Blue * Blue);

        public ColorF Normalize()
        {
            if (Equals(this, Presets.Black))
                return Presets.Black;

            var magnitude = Magnitude;
            return new ColorF(Red / magnitude, Green / magnitude, Blue / magnitude);
        }

        public ColorF Clamp() => new ColorF(Math.Min(Math.Max(0, Red), 1), Math.Min(Math.Max(0, Green), 1), Math.Min(Math.Max(0, Blue), 1));
    }
}