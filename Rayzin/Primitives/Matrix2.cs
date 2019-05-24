using System;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    [PublicAPI]
    public struct Matrix2
    {
        public Matrix2(double m00, double m01, double m10, double m11) => (M00, M01, M10, M11) = (m00, m01, m10, m11);

        public readonly double M00, M01;
        public readonly double M10, M11;

        public double this[int row, int column]
        {
            get
            {
                switch (row)
                {
                    case 0:
                        switch (column)
                        {
                            case 0:
                                return M00;

                            case 1:
                                return M01;
                        }

                        break;

                    case 1:
                        switch (column)
                        {
                            case 0:
                                return M10;

                            case 1:
                                return M11;
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(row), "row must be in the range 0..1");
                }

                throw new ArgumentOutOfRangeException(nameof(column), "column must be in the range 0..1");
            }
        }

        public Tuple2 Row(int row)
        {
            switch (row)
            {
                case 0:
                    return new Tuple2(M00, M01);

                case 1:
                    return new Tuple2(M10, M11);

                default:
                    throw new ArgumentOutOfRangeException(nameof(row), "row must be in the range 0..1");
            }
        }

        public Tuple2 Column(int column)
        {
            switch (column)
            {
                case 0:
                    return new Tuple2(M00, M10);

                case 1:
                    return new Tuple2(M01, M11);

                default:
                    throw new ArgumentOutOfRangeException(nameof(column), "row must be in the range 0..1");
            }
        }

        public static Matrix2 operator *(Matrix2 a, Matrix2 b)
        {
            Tuple2 aRow0 = a.Row(0);
            Tuple2 aRow1 = a.Row(1);

            Tuple2 bCol0 = b.Column(0);
            Tuple2 bCol1 = b.Column(1);

            return new Matrix2(aRow0 * bCol0, aRow0 * bCol1, aRow1 * bCol0, aRow1 * bCol1);
        }
    }
}