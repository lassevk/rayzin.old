using System;
using System.Linq;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    public readonly struct RzTuple : IEquatable<RzTuple>
    {
        private readonly double[] _Values;
        
        public RzTuple([NotNull] params double[] values)
        {
            _Values = values.ToArray();
        }

        public double this[int index] => _Values?[index] ?? 0;

        public int Length => _Values?.Length ?? 0;

        public static implicit operator RzTuple(RzPoint p) => new RzTuple(p.X, p.Y, p.Z, 1);
        public static implicit operator RzTuple(RzVector v) => new RzTuple(v.X, v.Y, v.Z, 0);

        public static explicit operator RzPoint(RzTuple t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (t._Values[3] != 1)
                throw new InvalidOperationException("A RzTuple must have W ([3]) = 1 to cast to a RzPoint");

            return new RzPoint(t._Values[0], t._Values[1], t._Values[2]);
        }

        public static explicit operator RzVector(RzTuple t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (t._Values[3] != 0)
                throw new InvalidOperationException("A RzTuple must have W ([3]) = 0 to cast to a RzVector");

            return new RzVector(t._Values[0], t._Values[1], t._Values[2]);
        }

        public bool Equals(RzTuple other)
        {
            if (_Values?.Length != other._Values?.Length)
                return false;

            if (_Values is null || other._Values is null)
                return false;
            
            for (int index = 0; index < _Values.Length; index++)
                if (!RzEpsilon.Equals(_Values[index], other._Values[index]))
                    return false;

            return true;
        }

        public override bool Equals(object obj) => obj is RzTuple other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzTuple left, RzTuple right) => left.Equals(right);

        public static bool operator !=(RzTuple left, RzTuple right) => !left.Equals(right);

        public override string ToString() => $"({string.Join(", ", _Values)})";
    }
}