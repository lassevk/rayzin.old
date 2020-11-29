using System;
using System.Linq;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    public readonly struct TupleF : IEquatable<TupleF>
    {
        private readonly double[] _Values;
        
        public TupleF([NotNull] params double[] values)
        {
            _Values = values.ToArray();
        }

        public double this[int index] => _Values?[index] ?? 0;

        public int Length => _Values?.Length ?? 0;

        public static implicit operator TupleF(Point3D p) => new TupleF(p.X, p.Y, p.Z, 1);
        public static implicit operator TupleF(Vector3D v) => new TupleF(v.X, v.Y, v.Z, 0);

        public static explicit operator Point3D(TupleF t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (t._Values[3] != 1)
                throw new InvalidOperationException("A TupleF must have W ([3]) = 1 to cast to a Point3D");

            return new Point3D(t._Values[0], t._Values[1], t._Values[2]);
        }

        public static explicit operator Vector3D(TupleF t)
        {
            if (t.Length != 4)
                throw new InvalidOperationException();

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (t._Values[3] != 0)
                throw new InvalidOperationException("A TupleF must have W ([3]) = 0 to cast to a Vector3D");

            return new Vector3D(t._Values[0], t._Values[1], t._Values[2]);
        }

        public bool Equals(TupleF other)
        {
            if (_Values?.Length != other._Values?.Length)
                return false;

            if (_Values is null || other._Values is null)
                return false;
            
            for (int index = 0; index < _Values.Length; index++)
                if (!Epsilon.Equals(_Values[index], other._Values[index]))
                    return false;

            return true;
        }

        public override bool Equals(object obj) => obj is TupleF other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(TupleF left, TupleF right) => left.Equals(right);

        public static bool operator !=(TupleF left, TupleF right) => !left.Equals(right);

        public override string ToString() => $"({string.Join(", ", _Values)})";
    }
}