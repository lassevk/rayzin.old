using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rayzin.Primitives
{
    public readonly struct Intersections : IEnumerable<Intersection>
    {
        private readonly Intersection[] _Intersections;

        public Intersections(params Intersection[] intersections)
            => _Intersections = (intersections ?? throw new ArgumentNullException(nameof(intersections))).ToArray();

        public Intersection this[int index] => _Intersections?[index] ?? default;

        public int Count => _Intersections?.Length ?? 0;

        public Intersection? Hit()
        {
            if (_Intersections is null || _Intersections.Length == 0)
                return null;

            Intersection? result = null;
            foreach (Intersection intersection in _Intersections)
            {
                if (intersection.Time < 0)
                    continue;

                if (result is null || intersection.Time < result.Value.Time)
                    result = intersection;
            }

            return result;
        }

        public IEnumerator<Intersection> GetEnumerator()
        {
            if (_Intersections is null)
                yield break;

            foreach (Intersection intersection in _Intersections)
                yield return intersection;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}