using System;
using System.Text;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    public readonly struct MatrixF : IEquatable<MatrixF>
    {
        [NotNull]
        private readonly double[] _Values;

        public struct Presets
        {
            public static readonly MatrixF Identity4 = MatrixF.Identity(4);
            public static readonly MatrixF Identity4Inverse = MatrixF.Identity(4).Inverse();
        }

        public MatrixF(int size, [NotNull] params double[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.Length != size * size)
                throw new ArgumentOutOfRangeException(
                    nameof(values), $"values must have a length corresponding to size*size (={size * size}) but was {values.Length}");

            _Values = new double[size * size];
            for (var index = 0; index < size * size; index++)
                _Values[index] = values[index];

            Size = size;
        }

        public int Size { get; }

        public double this[int column, int row] => _Values[column * Size + row];

        public override string ToString()
        {
            var result = new StringBuilder();
            for (var y = 0; y < Size; y++)
            {
                if (result.Length > 0)
                    result.AppendLine("");

                result.Append("| ");
                for (var x = 0; x < Size; x++)
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

            for (var index = 0; index < Size*Size; index++)
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
            for (var y = 0; y < a.Size; y++)
                for (var x = 0; x < a.Size; x++)
                {
                    double sum = 0;
                    for (var index = 0; index < a.Size; index++)
                        sum += a[y, index] * b[index, x];

                    result[y * a.Size + x] = sum;
                }

            return new MatrixF(4, result);
        }

        public static Point3D operator *(MatrixF m, Point3D p) => (Point3D)(m * (TupleF)p);
        public static Vector3D operator *(MatrixF m, Vector3D v) => (Vector3D)(m * (TupleF)v);
        public static TupleF operator *(MatrixF m, TupleF t)
        {
            if (m.Size != t.Length)
                throw new InvalidOperationException(
                    $"A matrix multiplied by a tuple must have the same size vs. length, actual were {m.Size} vs. {t.Length}");

            var result = new double[t.Length];
            for (var x = 0; x < t.Length; x++)
            {
                double sum = 0;
                for (var y = 0; y < m.Size; y++)
                    sum += m[x, y] * t[y];

                result[x] = sum;
            }

            return new TupleF(result);
        }

        public static MatrixF Identity(int size)
        {
            var values = new double[size * size];
            for (var index = 0; index < size; index++)
                values[index * size + index] = 1;

            return new MatrixF(size, values);
        }

        public MatrixF Transpose()
        {
            var values = new double[Size * Size];
            for (var x = 0; x < Size; x++)
                for (var y = 0; y < Size; y++)
                    values[x * Size + y] = _Values[y * Size + x];

            return new MatrixF(Size, values);
        }

        public double Determinant()
        {
            switch (Size)
            {
                case 2:
                    return _Values[0] * _Values[3] - _Values[1] * _Values[2];
                
                default:
                    double result = 0;
                    for (var x = 0; x < Size; x++)
                        result += _Values[x] * CoFactor(0, x);

                    return result;
            }
        }

        public MatrixF SubMatrix(int row, int column)
        {
            var newSize = Size - 1;
            var values = new double[newSize * newSize];
            var y2 = 0;
            for (var y1 = 0; y1 < Size; y1++)
            {
                if (y1 == column)
                    continue;

                var x2 = 0;
                for (var x1 = 0; x1 < Size; x1++)
                {
                    if (x1 == row)
                        continue;

                    values[x2 * newSize + y2] = _Values[x1 * Size + y1];
                    x2++;
                }

                y2++;
            }

            return new MatrixF(newSize, values);
        }

        public double Minor(int row, int column) => SubMatrix(row, column).Determinant();

        public double CoFactor(int row, int column)
        {
            var minor = Minor(row, column);
            if ((row + column) % 2 != 0)
                return -minor;

            return minor;
        }

        public MatrixF CoFactors()
        {
            var cofactors = new double[Size * Size];
            for (var row = 0; row < Size; row++)
                for (var column = 0; column < Size; column++)
                    cofactors[row * Size + column] = CoFactor(row, column);

            return new MatrixF(Size, cofactors);
        }

        public bool IsInvertible() => Determinant() != 0;

        public MatrixF Inverse()
        {
            var determinant = Determinant();
            if (determinant == 0)
                throw new InvalidOperationException("Matrix is not invertible, has a zero determinant");

            MatrixF cofactors = CoFactors();
            MatrixF transposedCofactors = cofactors.Transpose();

            var values = new double[Size * Size];
            for (var row = 0; row < Size; row++)
                for (var column = 0; column < Size; column++)
                    values[column * Size + row] = transposedCofactors[column, row] / determinant;

            return new MatrixF(4, values);
        }

        public MatrixF RotateX(double radians) => Transforms.RotationX(radians) * this;
        public MatrixF RotateY(double radians) => Transforms.RotationY(radians) * this;
        public MatrixF RotateZ(double radians) => Transforms.RotationZ(radians) * this;
        public MatrixF Scale(double x, double y, double z) => Transforms.Scaling(x, y, z) * this;
        public MatrixF Translate(double x, double y, double z) => Transforms.Translation(x, y, z) * this;
    }
}