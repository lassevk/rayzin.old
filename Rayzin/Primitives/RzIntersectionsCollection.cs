using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Rayzin.Primitives
{
    public readonly struct RzIntersectionsCollection : IEnumerable<RzIntersection>
    {
        private readonly RzIntersection[] _Intersections;

        public RzIntersectionsCollection(params RzIntersection[] intersections)
            => _Intersections = (intersections ?? throw new ArgumentNullException(nameof(intersections))).ToArray();

        public RzIntersection this[int index] => _Intersections?[index] ?? default;

        public int Count => _Intersections?.Length ?? 0;

        public RzIntersection? Hit()
        {
            if (_Intersections is null || _Intersections.Length == 0)
                return null;

            RzIntersection? result = null;
            foreach (RzIntersection intersection in _Intersections)
            {
                if (intersection.Time < 0)
                    continue;

                if (result is null || intersection.Time < result.Value.Time)
                    result = intersection;
            }

            return result;
        }

        public IEnumerator<RzIntersection> GetEnumerator()
        {
            if (_Intersections is null)
                yield break;

            foreach (RzIntersection intersection in _Intersections)
                yield return intersection;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}