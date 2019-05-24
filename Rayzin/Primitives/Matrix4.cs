using System;

using JetBrains.Annotations;

namespace Rayzin.Primitives
{
    [PublicAPI]
    public struct Matrix4
    {
        public Matrix4(
            double m00, double m01, double m02, double m03, double m10, double m11, double m12, double m13, double m20, double m21,
            double m22, double m23, double m30, double m31, double m32, double m33)
        {
            M00 = m00;
            M01 = m01;
            M02 = m02;
            M03 = m03;
            M10 = m10;
            M11 = m11;
            M12 = m12;
            M13 = m13;
            M20 = m20;
            M21 = m21;
            M22 = m22;
            M23 = m23;
            M30 = m30;
            M31 = m31;
            M32 = m32;
            M33 = m33;
        }

        public readonly double M00, M01, M02, M03;
        public readonly double M10, M11, M12, M13;
        public readonly double M20, M21, M22, M23;
        public readonly double M30, M31, M32, M33;

        public Tuple4 Row(int row)
        {
            switch (row)
            {
                case 0:
                    return new Tuple4(M00, M01, M02, M03);

                case 1:
                    return new Tuple4(M10, M11, M12, M13);

                case 2:
                    return new Tuple4(M20, M21, M22, M23);

                case 3:
                    return new Tuple4(M30, M31, M32, M33);

                default:
                    throw new ArgumentOutOfRangeException(nameof(row), "row must be in the range 0..3");
            }
        }

        public Tuple4 Column(int column)
        {
            switch (column)
            {
                case 0:
                    return new Tuple4(M00, M10, M20, M30);

                case 1:
                    return new Tuple4(M01, M11, M21, M31);

                case 2:
                    return new Tuple4(M02, M12, M22, M32);

                case 3:
                    return new Tuple4(M03, M13, M23, M33);

                default:
                    throw new ArgumentOutOfRangeException(nameof(column), "row must be in the range 0..3");
            }
        }

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

                            case 2:
                                return M02;

                            case 3:
                                return M03;
                        }

                        break;

                    case 1:
                        switch (column)
                        {
                            case 0:
                                return M10;

                            case 1:
                                return M11;

                            case 2:
                                return M12;

                            case 3:
                                return M13;
                        }

                        break;

                    case 2:
                        switch (column)
                        {
                            case 0:
                                return M20;

                            case 1:
                                return M21;

                            case 2:
                                return M22;

                            case 3:
                                return M23;
                        }

                        break;

                    case 3:
                        switch (column)
                        {
                            case 0:
                                return M30;

                            case 1:
                                return M31;

                            case 2:
                                return M32;

                            case 3:
                                return M33;
                        }

                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(row), "row must be in the range 0..3");
                }

                throw new ArgumentOutOfRangeException(nameof(column), "column must be in the range 0..3");
            }
        }

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            Tuple4 aRow0 = a.Row(0);
            Tuple4 aRow1 = a.Row(1);
            Tuple4 aRow2 = a.Row(2);
            Tuple4 aRow3 = a.Row(3);

            Tuple4 bCol0 = b.Column(0);
            Tuple4 bCol1 = b.Column(1);
            Tuple4 bCol2 = b.Column(2);
            Tuple4 bCol3 = b.Column(3);

            return new Matrix4(
                aRow0 * bCol0, aRow0 * bCol1, aRow0 * bCol2, aRow0 * bCol3, aRow1 * bCol0, aRow1 * bCol1, aRow1 * bCol2, aRow1 * bCol3,
                aRow2 * bCol0, aRow2 * bCol1, aRow2 * bCol2, aRow2 * bCol3, aRow3 * bCol0, aRow3 * bCol1, aRow3 * bCol2, aRow3 * bCol3);
        }
    }
}