using NUnit.Framework;
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Core.Tests
{
    [TestFixture]
    public class Vector3DTests
    {
        [Test]
        [TestCase(4.3, -4.2, 3.1)]
        public void Constructor_WithTestCases_ProducesExpectedResults(double x, double y, double z)
        {
            var p = new Vector3D(x, y, z);

            Assert.That(p.X, Is.EqualTo(x).Within(1e-5));
            Assert.That(p.Y, Is.EqualTo(y).Within(1e-5));
            Assert.That(p.Z, Is.EqualTo(z).Within(1e-5));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, -3.1, false)]
        public void Equals_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new Vector3D(x1, y1, z1);
            var p2 = new Vector3D(x2, y2, z2);

            bool output = p1.Equals(p2);

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, -3.1, false)]
        public void EqualsObject_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new Vector3D(x1, y1, z1);
            var p2 = new Vector3D(x2, y2, z2);

            bool output = p1.Equals((object)p2);

            Assert.That(output, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, -3.1, false)]
        public void EqualsOperators_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new Vector3D(x1, y1, z1);
            var p2 = new Vector3D(x2, y2, z2);

            if (expected)
                Assert.That(p1 == p2, Is.True);
            else
                Assert.That(p1 != p2, Is.True);
        }
 
        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(1, 0, 0, -1, 0, 0, false)]
        [TestCase(1, 2, 3, 2, 3, 1, false)]
        public void GetHashCode_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new Vector3D(x1, y1, z1);
            var p2 = new Vector3D(x2, y2, z2);

            var hashCode1 = p1.GetHashCode();
            var hashCode2 = p2.GetHashCode();
            bool output = hashCode1.Equals(hashCode2);

            Assert.That(output, Is.EqualTo(expected));
        }
        
        [Test]
        public void Add_TwoVectors_ProducesVector()
        {
            var v1 = new Vector3D(4, 3, 2);
            var v2 = new Vector3D(3, 2, 1);

            Vector3D v3 = v1 + v2;

            Assert.That(v3.X, Is.EqualTo(7).Within(1e-5));
            Assert.That(v3.Y, Is.EqualTo(5).Within(1e-5));
            Assert.That(v3.Z, Is.EqualTo(3).Within(1e-5));
        }

        [Test]
        public void Subtract_TwoVectors_ProducesVector()
        {
            var v1 = new Vector3D(4, 3, 2);
            var v2 = new Vector3D(1, 2, 3);

            Vector3D v3 = v1 - v2;

            Assert.That(v3.X, Is.EqualTo(3).Within(1e-5));
            Assert.That(v3.Y, Is.EqualTo(1).Within(1e-5));
            Assert.That(v3.Z, Is.EqualTo(-1).Within(1e-5));
        }

        [Test]
        public void Negative_Vector_ProducesNegativeVector()
        {
            var v1 = new Vector3D(4.3, -4.2, 3.1);

            Vector3D v2 = -v1;
            
            Assert.That(v2.X, Is.EqualTo(-4.3).Within(1e-5));
            Assert.That(v2.Y, Is.EqualTo(4.2).Within(1e-5));
            Assert.That(v2.Z, Is.EqualTo(-3.1).Within(1e-5));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 3.5, 4.3 * 3.5, -4.2 * 3.5, 3.1 * 3.5)]
        [TestCase(4.3, -4.2, 3.1, 0.5, 2.15, -2.1, 1.55)]
        public void Multiply_VectorByScalar_ProducesMultipliedVector(double x, double y, double z, double scalar, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new Vector3D(x, y, z);

            Vector3D v2 = v1 * scalar;
            
            Assert.That(v2.X, Is.EqualTo(expectedX).Within(1e-5));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(1e-5));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(1e-5));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 3.5, 4.3 * 3.5, -4.2 * 3.5, 3.1 * 3.5)]
        [TestCase(4.3, -4.2, 3.1, 0.5, 2.15, -2.1, 1.55)]
        public void Multiply_ScalarByVector_ProducesMultipliedVector(double x, double y, double z, double scalar, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new Vector3D(x, y, z);

            Vector3D v2 = scalar * v1;
            
            Assert.That(v2.X, Is.EqualTo(expectedX).Within(1e-5));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(1e-5));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(1e-5));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 0.5, 8.6, -8.4, 6.2)]
        [TestCase(4.3, -4.2, 3.1, 2, 2.15, -2.1, 1.55)]
        public void Divide_VectorByScalar_ProducesMultipliedVector(double x, double y, double z, double scalar, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new Vector3D(x, y, z);

            Vector3D v2 = v1 / scalar;
            
            Assert.That(v2.X, Is.EqualTo(expectedX).Within(1e-5));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(1e-5));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(1e-5));
        }

        [Test]
        [TestCase(0, 0, 0, 0)]
        [TestCase(1, 0, 0, 1)]
        [TestCase(0, 1, 0, 1)]
        [TestCase(0, 0, 1, 1)]
        [TestCase(1, 1, 0, 1.414213562373095)]
        [TestCase(1, 2, 3, 3.741657386773941)]
        [TestCase(-1, -2, -3, 3.741657386773941)]
        public void Magnitude_WithTestCases_ProducesCorrectResults(double x, double y, double z, double expected)
        {
            var v = new Vector3D(x, y, z);

            Assert.That(v.Magnitude, Is.EqualTo(expected).Within(1e-5));
        }

        [Test]
        [TestCase(1, 2, 3, 0.26726, 0.53452, 0.80178)]
        [TestCase(10, 0, 0, 1, 0, 0)]
        [TestCase(0, 10, 0, 0, 1, 0)]
        [TestCase(0, 0, 10, 0, 0, 1)]
        public void Normalize_WithTestCases_ProducesCorrectResults(
            double x, double y, double z, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new Vector3D(x, y, z);

            var v2 = v1.Normalize();

            Assert.That(v2.X, Is.EqualTo(expectedX).Within(1e-5));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(1e-5));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(1e-5));
        }

        [Test]
        [TestCase(1, 2, 3, 2, 3, 4, 20)]
        public void DotProduct_WithTestCases_ProducesCorrectResults(
            double x1, double y1, double z1, double x2, double y2, double z2, double expected)
        {
            var v1 = new Vector3D(x1, y1, z1);
            var v2 = new Vector3D(x2, y2, z2);

            var output = v1.Dot(v2);

            Assert.That(output, Is.EqualTo(expected).Within(1e-5));
        }

        [Test]
        [TestCase(1, 2, 3, 2, 3, 4, -1, 2, -1)]
        [TestCase(2, 3, 4, 1, 2, 3, 1, -2, 1)]
        public void CrossProduct_WithTestCases_ProducesCorrectResults(
            double x1, double y1, double z1, double x2, double y2, double z2, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new Vector3D(x1, y1, z1);
            var v2 = new Vector3D(x2, y2, z2);

            var output = v1.Cross(v2);

            Assert.That(output.X, Is.EqualTo(expectedX).Within(1e-5));
            Assert.That(output.Y, Is.EqualTo(expectedY).Within(1e-5));
            Assert.That(output.Z, Is.EqualTo(expectedZ).Within(1e-5));
        }
    }
}