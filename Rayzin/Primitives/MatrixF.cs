using System;
using System.Linq;
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

        public static MatrixF operator *(MatrixF a, MatrixF b)
        {
            if (a.Size != b.Size)
                throw new InvalidOperationException(
                    $"Matrices that are multiplied must have the same size, actual were {a.Size} vs. {b.Size}");

            var result = new double[a.Size * a.Size];
            for (int y = 0; y < a.Size; y++)
                for (int x = 0; x < a.Size; x++)
                {
                    double sum = 0;
                    for (int index = 0; index < a.Size; index++)
                        sum += a[y, index] * b[index, x];

                    result[y * a.Size + x] = sum;
                }

            return new MatrixF(4, result);
        }

        public static TupleF operator *(MatrixF m, TupleF t)
        {
            if (m.Size != t.Length)
                throw new InvalidOperationException(
                    $"A matrix multiplied by a tuple must have the same size vs. length, actual were {m.Size} vs. {t.Length}");

            var result = new double[t.Length];
            for (int x = 0; x < t.Length; x++)
            {
                double sum = 0;
                for (int y = 0; y < m.Size; y++)
                    sum += m[x, y] * t[y];

                result[x] = sum;
            }

            return new TupleF(result);
        }

        public static MatrixF Identity(int size)
        {
            var values = new double[size * size];
            for (int index = 0; index < size; index++)
                values[index * size + index] = 1;

            return new MatrixF(size, values);
        }

        public MatrixF Transpose()
        {
            var values = new double[Size * Size];
            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                    values[x * Size + y] = _Values[y * Size + x];

            return new MatrixF(Size, values);
        }

        public double Determinant()
        {
            if (Size == 2)
                return _Values[0] * _Values[3] - _Values[1] * _Values[2];

            double sum = 0;
            for (int x = 0; x < 4; x++)
                sum += CoFactor(x, 0) * _Values[x];

            return sum;
        }

        public MatrixF SubMatrix(int row, int column)
        {
            int newSize = Size - 1;
            double[] values = new double[newSize * newSize];
            int y2 = 0;
            for (int y1 = 0; y1 < Size; y1++)
            {
                if (y1 == column)
                    continue;

                int x2 = 0;
                for (int x1 = 0; x1 < Size; x1++)
                {
                    if (x1 == row)
                        continue;

                    Console.WriteLine($"{x1}, {y1}, {x1 * Size + y1}");
                    if (x1 * Size + y1 < 0 || x1 * Size + y1 >= _Values.Length)
                    {
                        Console.WriteLine("Here");
                    }

                    double value = _Values[x1 * Size + y1];

                    if (x2 * newSize + y2 < 0 || x2 * newSize + y2 >= values.Length)
                    {
                        Console.WriteLine("Here");
                    }

                    values[x2 * newSize + y2] = value;
                    x2++;
                }

                y2++;
            }

            return new MatrixF(newSize, values);
        }

        public double Minor(int row, int column)
        {
            return SubMatrix(row, column).Determinant();
        }

        public double CoFactor(int row, int column)
        {
            var minor = Minor(row, column);
            if ((row + column) % 2 != 0)
                return -minor;

            return minor;
        }
    }
}