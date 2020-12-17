using System;

using Rayzin.Objects;
using Rayzin.Objects.Renderables;

namespace Rayzin.Primitives
{
    public readonly struct RzIntersection : IEquatable<RzIntersection>
    {
        public RzIntersection(RzRenderable obj, double time) => (Object, Time) = (obj ?? throw new ArgumentNullException(nameof(obj)), time);

        public RzRenderable Object { get; }

        public double Time { get; }

        public bool Equals(RzIntersection other) => Equals(Object, other.Object) && Time.Equals(other.Time);

        public override bool Equals(object obj) => obj is RzIntersection other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzIntersection left, RzIntersection right) => left.Equals(right);

        public static bool operator !=(RzIntersection left, RzIntersection right) => !left.Equals(right);

        public override string ToString() => $"{Object} at {Time:G}";
    }
}
