using System;

using Rayzin.Objects;

namespace Rayzin.Primitives
{
    public readonly struct RayF
    {
        public RayF(Point3D origin, Vector3D direction) => (Origin, Direction) = (origin, direction);

        public Point3D Origin { get; }

        public Vector3D Direction { get; }

        public Point3D Position(double t) => Origin + Direction * t;

        public Intersections Intersect(Object3D obj) => obj.Intersect(this);

        public RayF Transform(MatrixF transformation) => new RayF(transformation * Origin, transformation * Direction);
    }
}