using System;

using Rayzin.Objects;

namespace Rayzin.Primitives
{
    public readonly struct Intersection : IEquatable<Intersection>
    {
        public Intersection(Object3D obj, double time) => (Object, Time) = (obj ?? throw new ArgumentNullException(nameof(obj)), time);

        public Object3D Object { get; }

        public double Time { get; }

        public bool Equals(Intersection other) => Equals(Object, other.Object) && Time.Equals(other.Time);

        public override bool Equals(object obj) => obj is Intersection other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(Intersection left, Intersection right) => left.Equals(right);

        public static bool operator !=(Intersection left, Intersection right) => !left.Equals(right);
    }
}
