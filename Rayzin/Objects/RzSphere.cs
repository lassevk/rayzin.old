using System;

using Rayzin.Primitives;

namespace Rayzin.Objects
{
    public class RzSphere : RzObject
    {
        public override RzIntersectionsCollection Intersect(RzRay ray)
        {
            RzRay transformedRay = ray.Transform(InverseTransformation);
            RzVector sphereToRay = transformedRay.Origin - new RzPoint(0, 0, 0);
            var a = transformedRay.Direction.Dot(transformedRay.Direction);
            var b = 2 * transformedRay.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return new RzIntersectionsCollection();

            var a2 = 2 * a;
            var dSqrt = Math.Sqrt(discriminant);
            var t1 = (-b - dSqrt) / a2;
            var t2 = (-b + dSqrt) / a2;

            if (RzEpsilon.Equals(t1, t2))
                return new RzIntersectionsCollection(new RzIntersection(this, t1));

            return new RzIntersectionsCollection(new RzIntersection(this, t1), new RzIntersection(this, t2));
        }
    }
}
