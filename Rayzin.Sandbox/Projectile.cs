using Rayzin.Primitives;

namespace Rayzin.Sandbox
{
    internal readonly struct Projectile
    {
        public Projectile(RzPoint position, RzVector velocity) => (Position, Velocity) = (position, velocity);

        public RzPoint Position { get; }

        public RzVector Velocity { get; }
    }
}