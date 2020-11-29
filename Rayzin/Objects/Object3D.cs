using Rayzin.Primitives;

namespace Rayzin.Objects
{
    public abstract class Object3D
    {
        public abstract Intersections Intersect(RayF ray);
    }
}