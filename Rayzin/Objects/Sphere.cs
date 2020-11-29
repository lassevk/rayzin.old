using System;

using Rayzin.Primitives;

namespace Rayzin.Objects
{
    public class Sphere : Object3D
    {
        public override Intersections Intersect(RayF ray)
        {
            Vector3D sphereToRay = ray.Origin - new Point3D(0, 0, 0); // to be replaced with something else?
            var a = ray.Direction.Dot(ray.Direction);
            var b = 2 * ray.Direction.Dot(sphereToRay);
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
