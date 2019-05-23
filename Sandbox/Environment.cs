using Rayzin.Core;

namespace Sandbox
{
    public struct Environment
    {
        public Environment(Vector3D gravity, Vector3D wind) => (Gravity, Wind) = (gravity, wind);

        public Vector3D Gravity { get; }

        public Vector3D Wind { get; }
    }
}