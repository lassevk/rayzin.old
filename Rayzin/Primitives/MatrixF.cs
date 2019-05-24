using System;
using System.Text;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    public struct MatrixF : IEquatable<MatrixF>
    {
        [NotNull]
        private readonly double[] _Values;

        public MatrixF(int size, [NotNull] double[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.Length != size * size)
                throw new ArgumentOutOfRangeException(
                    nameof(values), $"values must have a length corresponding to size*size (={size * size}) but was {values.Length}");

            _Values = new double[size * size];
            for (int index = 0; index < size * size; index++)
                _Values[index] = values[index];

            Size = size;
        }

        public int Size { get; }

        public double this[int x, int y]
        {
            get => _Values[x * Size + y];
            set => _Values[x * Size + y] = value;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int y = 0; y < Size; y++)
            {
                if (result.Length > 0)
                    result.AppendLine("");

                result.Append("| ");
                for (int x = 0; x < Size; x++)
                {
                    if (x > 0)
                        result.Append(" | ");
                    result.Append(this[y, x]);
                }

                result.Append(" |");
            }

            return result.ToString();
        }

        public bool Equals(MatrixF other)
        {
            if (Size != other.Size)
                return false;

            for (int index = 0; index < Size*Size; index++)
                if (!Epsilon.Equals(_Values[index], other._Values[index]))
                    return false;

            return true;
        }

        public override bool Equals(object obj) => obj is MatrixF other && Equals(other);

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(MatrixF left, MatrixF right) => left.Equals(right);

        public static bool operator !=(MatrixF left, MatrixF right) => !left.Equals(right);
    }
}