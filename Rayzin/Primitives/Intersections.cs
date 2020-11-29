using System;
using System.Linq;

namespace Rayzin.Primitives
{
    public readonly struct Intersections
    {
        private readonly Intersection[] _Intersections;

        public Intersections(params Intersection[] intersections)
            => _Intersections = (intersections ?? throw new ArgumentNullException(nameof(intersections))).ToArray();

        public Intersection this[int index] => _Intersections?[index] ?? default;

        public int Count => _Intersections?.Length ?? 0;
    }
}