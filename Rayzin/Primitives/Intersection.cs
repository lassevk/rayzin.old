using System;

using Rayzin.Objects;

namespace Rayzin.Primitives
{
    public readonly struct Intersection
    {
        public Intersection(Object3D obj, double time) => (Object, Time) = (obj ?? throw new ArgumentNullException(nameof(obj)), time);

        public Object3D Object { get; }

        public double Time { get; }
    }
}
