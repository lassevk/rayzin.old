using System;

using Rayzin.Primitives;

namespace Rayzin.Objects
{
    public class Sphere : Object3D
    {
        public override Intersections Intersect(RayF ray)
        {
            RayF transformedRay = ray.Transform(InverseTransformation);
            Vector3D sphereToRay = transformedRay.Origin - new Point3D(0, 0, 0);
            var a = transformedRay.Direction.Dot(transformedRay.Direction);
            var b = 2 * transformedRay.Direction.Dot(sphereToRay);
            var c = sphereToRay.Dot(sphereToRay) - 1;
            var discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
                return new Intersections();

            var a2 = 2 * a;
            var dSqrt = Math.Sqrt(discriminant);
            var t1 = (-b - dSqrt) / a2;
            var t2 = (-b + dSqrt) / a2;

            if (Epsilon.Equals(t1, t2))
                return new Intersections(new Intersection(this, t1));

            return new Intersections(new Intersection(this, t1), new Intersection(this, t2));
        }
    }
}
