using System;

using Rayzin.Objects.Renderables;

namespace Rayzin.Primitives
{
    public readonly struct RzAnnotatedIntersection : IEquatable<RzAnnotatedIntersection>
    {
        public RzAnnotatedIntersection(RzIntersection intersection, RzRay ray)
        {
            Object = intersection.Object;
            Time = intersection.Time;
            Point = ray.Position(Time);
            EyeVector = -ray.Direction;
            NormalVector = intersection.Object.NormalAt(Point);

            if (NormalVector.Dot(EyeVector) < 0)
            {
                IsInside = true;
                NormalVector = -NormalVector;
            }
            else
                IsInside = false;
        }

        public RzRenderable Object { get; }

        public double Time { get; }

        public RzPoint Point { get; }

        public RzVector EyeVector { get; }

        public RzVector NormalVector { get; }

        public bool IsInside { get; }

        public bool Equals(RzAnnotatedIntersection other)
            => Equals(Object, other.Object)
            && Time.Equals(other.Time)
            && Point.Equals(other.Point)
            && EyeVector.Equals(other.EyeVector)
            && NormalVector.Equals(other.NormalVector);

        public override bool Equals(object obj) => obj is RzAnnotatedIntersection other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(Object, Time, Point, EyeVector, NormalVector);

        public static bool operator ==(RzAnnotatedIntersection left, RzAnnotatedIntersection right) => left.Equals(right);

        public static bool operator !=(RzAnnotatedIntersection left, RzAnnotatedIntersection right) => !left.Equals(right);
    }
}