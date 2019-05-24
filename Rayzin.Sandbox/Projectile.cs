using Rayzin.Primitives;

namespace Rayzin.Sandbox
{
    internal struct Projectile
    {
        public Projectile(Point3D position, Vector3D velocity) => (Position, Velocity) = (position, velocity);

        public Point3D Position { get; }

        public Vector3D Velocity { get; }
    }
}