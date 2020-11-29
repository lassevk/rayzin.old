using Rayzin.Primitives;

namespace Rayzin.Sandbox
{
    public struct Environment
    {
        public Environment(RzVector gravity, RzVector wind) => (Gravity, Wind) = (gravity, wind);

        public RzVector Gravity { get; }

        public RzVector Wind { get; }
    }
}