using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RzVectorTests
    {
        [Test]
        [TestCase(4.3, -4.2, 3.1)]
        public void Constructor_WithTestCases_ProducesExpectedResults(double x, double y, double z)
        {
            var p = new RzVector(x, y, z);

            Assert.That(p.X, Is.EqualTo(x).Within(RzEpsilon.Value));
            Assert.That(p.Y, Is.EqualTo(y).Within(RzEpsilon.Value));
            Assert.That(p.Z, Is.EqualTo(z).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, -3.1, false)]
        public void Equals_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new RzVector(x1, y1, z1);
            var p2 = new RzVector(x2, y2, z2);

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
            var p1 = new RzVector(x1, y1, z1);
            var p2 = new RzVector(x2, y2, z2);

            bool output = p1.Equals((object)p2);

            Assert.That(output, Is.EqualTo(expected));
        }
        
        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, -3.1, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.2, 0.6, 0.7, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.3, 0.7, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.3, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5+RzEpsilon.Value/2, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5-RzEpsilon.Value/2, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6+RzEpsilon.Value/2, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6-RzEpsilon.Value/2, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7+RzEpsilon.Value/2, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7-RzEpsilon.Value/2, true)]
        public void EqualsOperators_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new RzVector(x1, y1, z1);
            var p2 = new RzVector(x2, y2, z2);

            if (expected)
                Assert.That(p1 == p2, Is.True);
            else
                Assert.That(p1 != p2, Is.True);
        }
 
        [Test]
        public void Add_TwoVectors_ProducesVector()
        {
            var v1 = new RzVector(4, 3, 2);
            var v2 = new RzVector(3, 2, 1);

            RzVector v3 = v1 + v2;

            Assert.That(v3.X, Is.EqualTo(7).Within(RzEpsilon.Value));
            Assert.That(v3.Y, Is.EqualTo(5).Within(RzEpsilon.Value));
            Assert.That(v3.Z, Is.EqualTo(3).Within(RzEpsilon.Value));
        }

        [Test]
        public void Subtract_TwoVectors_ProducesVector()
        {
            var v1 = new RzVector(4, 3, 2);
            var v2 = new RzVector(1, 2, 3);

            RzVector v3 = v1 - v2;

            Assert.That(v3.X, Is.EqualTo(3).Within(RzEpsilon.Value));
            Assert.That(v3.Y, Is.EqualTo(1).Within(RzEpsilon.Value));
            Assert.That(v3.Z, Is.EqualTo(-1).Within(RzEpsilon.Value));
        }

        [Test]
        public void Negative_Vector_ProducesNegativeVector()
        {
            var v1 = new RzVector(4.3, -4.2, 3.1);

            RzVector v2 = -v1;
            
            Assert.That(v2.X, Is.EqualTo(-4.3).Within(RzEpsilon.Value));
            Assert.That(v2.Y, Is.EqualTo(4.2).Within(RzEpsilon.Value));
            Assert.That(v2.Z, Is.EqualTo(-3.1).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 3.5, 4.3 * 3.5, -4.2 * 3.5, 3.1 * 3.5)]
        [TestCase(4.3, -4.2, 3.1, 0.5, 2.15, -2.1, 1.55)]
        public void Multiply_VectorByScalar_ProducesMultipliedVector(double x, double y, double z, double scalar, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new RzVector(x, y, z);

            RzVector v2 = v1 * scalar;
            
            Assert.That(v2.X, Is.EqualTo(expectedX).Within(RzEpsilon.Value));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(RzEpsilon.Value));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 3.5, 4.3 * 3.5, -4.2 * 3.5, 3.1 * 3.5)]
        [TestCase(4.3, -4.2, 3.1, 0.5, 2.15, -2.1, 1.55)]
        public void Multiply_ScalarByVector_ProducesMultipliedVector(double x, double y, double z, double scalar, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new RzVector(x, y, z);

            RzVector v2 = scalar * v1;
            
            Assert.That(v2.X, Is.EqualTo(expectedX).Within(RzEpsilon.Value));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(RzEpsilon.Value));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 0.5, 8.6, -8.4, 6.2)]
        [TestCase(4.3, -4.2, 3.1, 2, 2.15, -2.1, 1.55)]
        public void Divide_VectorByScalar_ProducesMultipliedVector(double x, double y, double z, double scalar, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new RzVector(x, y, z);

            RzVector v2 = v1 / scalar;
            
            Assert.That(v2.X, Is.EqualTo(expectedX).Within(RzEpsilon.Value));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(RzEpsilon.Value));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(RzEpsilon.Value));
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
            var v = new RzVector(x, y, z);

            Assert.That(v.Magnitude, Is.EqualTo(expected).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(1, 2, 3, 0.26726, 0.53452, 0.80178)]
        [TestCase(10, 0, 0, 1, 0, 0)]
        [TestCase(0, 10, 0, 0, 1, 0)]
        [TestCase(0, 0, 10, 0, 0, 1)]
        public void Normalize_WithTestCases_ProducesCorrectResults(
            double x, double y, double z, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new RzVector(x, y, z);

            var v2 = v1.Normalize();

            Assert.That(v2.X, Is.EqualTo(expectedX).Within(RzEpsilon.Value));
            Assert.That(v2.Y, Is.EqualTo(expectedY).Within(RzEpsilon.Value));
            Assert.That(v2.Z, Is.EqualTo(expectedZ).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(1, 2, 3, 2, 3, 4, 20)]
        public void DotProduct_WithTestCases_ProducesCorrectResults(
            double x1, double y1, double z1, double x2, double y2, double z2, double expected)
        {
            var v1 = new RzVector(x1, y1, z1);
            var v2 = new RzVector(x2, y2, z2);

            var output = v1.Dot(v2);

            Assert.That(output, Is.EqualTo(expected).Within(RzEpsilon.Value));
        }

        [Test]
        [TestCase(1, 2, 3, 2, 3, 4, -1, 2, -1)]
        [TestCase(2, 3, 4, 1, 2, 3, 1, -2, 1)]
        public void CrossProduct_WithTestCases_ProducesCorrectResults(
            double x1, double y1, double z1, double x2, double y2, double z2, double expectedX, double expectedY, double expectedZ)
        {
            var v1 = new RzVector(x1, y1, z1);
            var v2 = new RzVector(x2, y2, z2);

            var output = v1.Cross(v2);

            Assert.That(output.X, Is.EqualTo(expectedX).Within(RzEpsilon.Value));
            Assert.That(output.Y, Is.EqualTo(expectedY).Within(RzEpsilon.Value));
            Assert.That(output.Z, Is.EqualTo(expectedZ).Within(RzEpsilon.Value));
        }

        [Test]
        public void Deconstruct_Vector3D_ProducesExpectedResults()
        {
            var v = new RzVector(4, 3, 2);

            (var x, var y, var z) = v;

            Assert.That(x, Is.EqualTo(4).Within(RzEpsilon.Value));
            Assert.That(y, Is.EqualTo(3).Within(RzEpsilon.Value));
            Assert.That(z, Is.EqualTo(2).Within(RzEpsilon.Value));
        }
    }
}