using System.Runtime.CompilerServices;

using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class MatrixFTests
    {
        [Test]
        public void Constructor_4x4_ProducesExpectedResults()
        {
            var m = new MatrixF(4, new[] { 1, 2, 3, 4, 5.5, 6.5, 7.5, 8.5, 9, 10, 11, 12, 13.5, 14.5, 15.5, 16.5 });

            Assert.That(m[0, 0], Is.EqualTo(1).Within(Epsilon.Value));
            Assert.That(m[0, 3], Is.EqualTo(4).Within(Epsilon.Value));
            Assert.That(m[1, 0], Is.EqualTo(5.5).Within(Epsilon.Value));
            Assert.That(m[1, 2], Is.EqualTo(7.5).Within(Epsilon.Value));
            Assert.That(m[2, 2], Is.EqualTo(11).Within(Epsilon.Value));
            Assert.That(m[3, 0], Is.EqualTo(13.5).Within(Epsilon.Value));
            Assert.That(m[3, 2], Is.EqualTo(15.5).Within(Epsilon.Value));
        }

        [Test]
        public void Constructor_2x2_ProducesExpectedResults()
        {
            var m = new MatrixF(2, new double[] { -3, 5, 1, -2 });

            Assert.That(m[0, 0], Is.EqualTo(-3).Within(Epsilon.Value));
            Assert.That(m[0, 1], Is.EqualTo(5).Within(Epsilon.Value));
            Assert.That(m[1, 0], Is.EqualTo(1).Within(Epsilon.Value));
            Assert.That(m[1, 1], Is.EqualTo(-2).Within(Epsilon.Value));
        }

        [Test]
        public void Constructor_3x3_ProducesExpectedResults()
        {
            var m = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });

            Assert.That(m[0, 0], Is.EqualTo(-3).Within(Epsilon.Value));
            Assert.That(m[1, 1], Is.EqualTo(-2).Within(Epsilon.Value));
            Assert.That(m[2, 2], Is.EqualTo(1).Within(Epsilon.Value));
        }

        [Test]
        public void Equals_SameMatrix_ReturnsTrue()
        {
            var m = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });

            bool output = m.Equals(m);

            Assert.That(output, Is.True);
        }

        [Test]
        public void Equals_DifferentMatrixWithSameValues_ReturnsTrue()
        {
            var m1 = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });
            var m2 = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });

            bool output = m1.Equals(m2);

            Assert.That(output, Is.True);
        }

        [Test]
        public void Equals_DifferenceBiggerThanEpsilon_ReturnsFalse()
        {
            var m1 = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });
            var m2 = new MatrixF(3, new[] { -3 + Epsilon.Value * 2, 5, 0, 1, -2, -7, 0, 1, 1 });

            bool output = m1.Equals(m2);

            Assert.That(output, Is.False);
        }

        [Test]
        public void Equals_DifferenceLessThanEpsilon_ReturnsTrue()
        {
            var m1 = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });
            var m2 = new MatrixF(3, new[] { -3 + Epsilon.Value / 2, 5, 0, 1, -2, -7, 0, 1, 1 });

            bool output = m1.Equals(m2);

            Assert.That(output, Is.True);
        }

        [Test]
        public void Multiply_Two4x4Matrices_ProducesExpectedResults()
        {
            var m1 = new MatrixF(4, new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 8, 7, 6, 5, 4, 3, 2 });
            var m2 = new MatrixF(4, new double[] { -2, 1, 2, 3, 3, 2, 1, -1, 4, 3, 6, 5, 1, 2, 7, 8 });

            var actual = m1 * m2;

            var expected = new MatrixF(4, new double[] { 20, 22, 50, 48, 44, 54, 114, 108, 40, 58, 110, 102, 16, 26, 46, 42 });
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void Multiply_4x4MatrixWith4ElementTuple_ProducesExpectedResults()
        {
            var m = new MatrixF(4, new double[] { 1, 2, 3, 4, 2, 4, 4, 2, 8, 6, 4, 1, 0, 0, 0, 1 });
            var t = new TupleF(1, 2, 3, 1);

            var actual = m * t;

            Assert.That(actual, Is.EqualTo(new TupleF(18, 24, 33, 1)));
        }

        [Test]
        public void Multiply_MatrixByIdentityMatrix_ProducesOriginalMatrix()
        {
            var m = new MatrixF(4, new double[] { 0, 1, 2, 4, 1, 2, 4, 8, 2, 4, 8, 16, 4, 8, 16, 32 });

            var actual = m * MatrixF.Identity(4);

            Assert.That(actual, Is.EqualTo(m));
        }

        [Test]
        public void Multiply_IdentityMatrixByTuple_ProducesOriginalTuple()
        {
            var t = new TupleF(1, 2, 3, 4);

            var actual = MatrixF.Identity(4) * t;

            Assert.That(actual, Is.EqualTo(t));
        }

        [Test]
        public void Transpose_ExampleMatrix_ProducesExpectedResults()
        {
            var m = new MatrixF(4, new double[] { 0, 9, 3, 0, 9, 8, 0, 8, 1, 8, 5, 3, 0, 0, 5, 8 });

            var actual = m.Transpose();

            Assert.That(actual, Is.EqualTo(new MatrixF(4, new double[] { 0, 9, 1, 0, 9, 8, 8, 0, 3, 0, 5, 5, 0, 8, 3, 8 })));
        }

        [Test]
        public void Transpose_IdentityMatrix_ProducesExpectedResults()
        {
            var m = MatrixF.Identity(4);

            var actual = m.Transpose();

            Assert.That(actual, Is.EqualTo(m));
        }

        [Test]
        public void Determinant_2x2Matrix_ProducesExpectedResult()
        {
            var m = new MatrixF(2, new double[] { 1, 5, -3, 2 });

            var actual = m.Determinant();

            Assert.That(actual, Is.EqualTo(17));
        }

        [Test]
        public void Determinant_3x3Matrix_ProducesExpectedResult()
        {
            var m = new MatrixF(3, new double[] { 1, 2, 6, -5, 8, -4, 2, 6, 4 });

            var actual = m.Determinant();
            Assert.That(actual, Is.EqualTo(-196));
        }

        [Test]
        public void Determinant_4x4Matrix_ProducesExpectedResult()
        {
            var m = new MatrixF(4, new double[] { -2, -8, 3, 5, -3, 1, 7, 3, 1, 2, -9, 6, -6, 7, 7, -9 });

            var actual = m.Determinant();
            Assert.That(actual, Is.EqualTo(-4071));
        }

        [Test]
        public void SubMatrix_Of3x3Matrix_ProducesExpectedResult()
        {
            var m = new MatrixF(3, new double[] { 1, 5, 0, -3, 2, 7, 0, 6, -3 });

            var actual = m.SubMatrix(0, 2);

            Assert.That(actual, Is.EqualTo(new MatrixF(2, new double[] { -3, 2, 0, 6 })));
        }

        [Test]
        public void Minor_Of3x3MatrixAtCoordinates_ProducesExpectedResults()
        {
            var a = new MatrixF(3, new double[] { 3, 5, 0, 2, -1, -7, 6, -1, 5 });
            var b = a.SubMatrix(1, 0);

            var determinant = b.Determinant();
            var minor = a.Minor(1, 0);

            Assert.That(determinant, Is.EqualTo(25));
            Assert.That(minor, Is.EqualTo(25));
        }

        [Test]
        public void CoFactor_Of3x3MatrixAtCoordinates_ProducesExpectedResults()
        {
            var a = new MatrixF(3, new double[] { 3, 5, 0, 2, -1, -7, 6, -1, 5 });

            Assert.That(a.Minor(0, 0), Is.EqualTo(-12));
            Assert.That(a.CoFactor(0, 0), Is.EqualTo(-12));

            Assert.That(a.Minor(1, 0), Is.EqualTo(25));
            Assert.That(a.CoFactor(1, 0), Is.EqualTo(-25));
        }

        [Test]
        public void MatrixIsInvertible()
        {
            var a = new MatrixF(4, new double[] { 6, 4, 4, 4, 5, 5, 7, 6, 4, -9, 3, -7, 9, 1, 7, -6 });

            Assert.That(a.Determinant(), Is.EqualTo(-2120));
            Assert.That(a.IsInvertible(), Is.True);
        }

        [Test]
        public void MatrixIsNotInvertible()
        {
            var a = new MatrixF(4, new double[] { -4, 2, -2, -3, 9, 6, 2, 6, 0, -5, 1, -5, 0, 0, 0, 0 });

            Assert.That(a.Determinant(), Is.EqualTo(0));
            Assert.That(a.IsInvertible(), Is.False);
        }

        [Test]
        public void Invert_Of3x3Matrix_ProducesExpectedResults()
        {
            var a = new MatrixF(4, new double[] { -5, 2, 6, -8, 1, -5, 1, 8, 7, 7, -6, -7, 1, -3, 7, 4 });

            MatrixF b = a.Inverse();

            Assert.That(a.Determinant(), Is.EqualTo(532));
            Assert.That(a.CoFactor(2, 3), Is.EqualTo(-160));
            Assert.That(b[3, 2], Is.EqualTo(-160.0 / 532.0).Within(Epsilon.Value));
            Assert.That(a.CoFactor(3, 2), Is.EqualTo(105));
            Assert.That(b[2, 3], Is.EqualTo(105.0 / 532.0).Within(Epsilon.Value));
            Assert.That(
                b,
                Is.EqualTo(
                    new MatrixF(
                        4,
                        new double[]
                        {
                            0.21805, 0.45113, 0.24060, -0.04511, -0.80827, -1.45677, -0.44361, 0.52068, -0.07895, -0.22368, -0.05263,
                            0.19737, -0.52256, -0.81391, -0.30075, 0.30639
                        })));
        }
    }
}