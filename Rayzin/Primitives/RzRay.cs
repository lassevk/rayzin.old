using Rayzin.Objects;

namespace Rayzin.Primitives
{
    public readonly struct RzRay
    {
        public RzRay(RzPoint origin, RzVector direction) => (Origin, Direction) = (origin, direction);

        public RzPoint Origin { get; }

        public RzVector Direction { get; }

        public RzPoint Position(double t) => Origin + Direction * t;

        public RzIntersectionsCollection Intersect(RzObject obj) => obj.Intersect(this);

        public RzRay Transform(RzMatrix transformation) => new RzRay(transformation * Origin, transformation * Direction);
    }
}