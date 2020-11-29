using System;
using System.Drawing;

namespace Rayzin.Primitives
{
    public readonly struct RzColor : IEquatable<RzColor>
    {
        public struct Presets
        {
            public static readonly RzColor Black = (0, 0, 0);
            public static readonly RzColor Red = (1, 0, 0);
            public static readonly RzColor Green = (0, 1, 0);
            public static readonly RzColor Blue = (0, 0, 1);
            public static readonly RzColor White = (1, 1, 1);
        }

        public RzColor(double red, double green, double blue) => (Red, Green, Blue) = (red, green, blue);
        public void Deconstruct(out double red, out double green, out double blue) => (red, green, blue) = (Red, Green, Blue);

        public double Red { get; }

        public double Green { get; }

        public double Blue { get; }

        public bool Equals(RzColor other)
            => RzEpsilon.Equals(Red, other.Red) && RzEpsilon.Equals(Green, other.Green) && RzEpsilon.Equals(Blue, other.Blue);

        public override bool Equals(object obj) => obj is RzColor other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzColor left, RzColor right) => left.Equals(right);

        public static bool operator !=(RzColor left, RzColor right) => !left.Equals(right);

        public static RzColor operator +(RzColor c1, RzColor c2) => new RzColor(c1.Red + c2.Red, c1.Green + c2.Green, c1.Blue + c2.Blue);
        public static RzColor operator -(RzColor c1, RzColor c2) => new RzColor(c1.Red - c2.Red, c1.Green - c2.Green, c1.Blue - c2.Blue);
        public static RzColor operator -(RzColor c) => new RzColor(-c.Red, -c.Green, -c.Blue);

        public static RzColor operator *(RzColor c, double scalar) => new RzColor(c.Red * scalar, c.Green * scalar, c.Blue * scalar);
        public static RzColor operator *(double scalar, RzColor c) => new RzColor(c.Red * scalar, c.Green * scalar, c.Blue * scalar);
        public static RzColor operator *(RzColor c1, RzColor c2) => new RzColor(c1.Red * c2.Red, c1.Green * c2.Green, c1.Blue * c2.Blue);

        public static RzColor operator /(RzColor c, double scalar) => new RzColor(c.Red / scalar, c.Green / scalar, c.Blue / scalar);

        public override string ToString() => $"RzColor ({Red}, {Green}, {Blue})";

        public double Magnitude => Math.Sqrt(Red * Red + Green * Green + Blue * Blue);

        public RzColor Normalize()
        {
            if (Equals(this, Presets.Black))
                return Presets.Black;

            var magnitude = Magnitude;
            return new RzColor(Red / magnitude, Green / magnitude, Blue / magnitude);
        }

        public RzColor Clamp() => new RzColor(Math.Min(Math.Max(0, Red), 1), Math.Min(Math.Max(0, Green), 1), Math.Min(Math.Max(0, Blue), 1));

        public Color ToColor()
        {
            RzColor c = Clamp();
            return Color.FromArgb((int)(255 * c.Red), (int)(255 * c.Green), (int)(255 * c.Blue));
        }

        public static implicit operator RzColor((double red, double green, double blue) tuple) => new RzColor(tuple.red, tuple.green, tuple.blue);
        public static implicit operator (double red, double green, double blue)(RzColor color) => (color.Red, color.Green, color.Blue);

    }
}