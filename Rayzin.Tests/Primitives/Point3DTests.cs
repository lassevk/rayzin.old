using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class Point3DTests
    {
        [Test]
        [TestCase(4.3, -4.2, 3.1)]
        public void Constructor_WithTestCases_ProducesExpectedResults(double x, double y, double z)
        {
            var p = new Point3D(x, y, z);

            Assert.That(p.X, Is.EqualTo(x).Within(Epsilon.Value));
            Assert.That(p.Y, Is.EqualTo(y).Within(Epsilon.Value));
            Assert.That(p.Z, Is.EqualTo(z).Within(Epsilon.Value));
        }

        [Test]
        [TestCase(4.3, -4.2, 3.1, 4.3, -4.2, 3.1, true)]
        [TestCase(4.3, -4.2, 3.1, -4.3, -4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, 3.1, false)]
        [TestCase(4.3, -4.2, 3.1, 4.3, 4.2, -3.1, false)]
        public void Equals_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new Point3D(x1, y1, z1);
            var p2 = new Point3D(x2, y2, z2);

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
            var p1 = new Point3D(x1, y1, z1);
            var p2 = new Point3D(x2, y2, z2);

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
        [TestCase(0.5, 0.6, 0.7, 0.5+Epsilon.Value/2, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5-Epsilon.Value/2, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6+Epsilon.Value/2, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6-Epsilon.Value/2, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7+Epsilon.Value/2, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7-Epsilon.Value/2, true)]
        public void EqualsOperators_WithTestCases_ProducesExpectedResults(double x1, double y1, double z1, double x2, double y2, double z2, bool expected)
        {
            var p1 = new Point3D(x1, y1, z1);
            var p2 = new Point3D(x2, y2, z2);

            if (expected)
                Assert.That(p1 == p2, Is.True);
            else
                Assert.That(p1 != p2, Is.True);
        }
 
        [Test]
        public void Subtract_TwoPoints_ProducesVector()
        {
            var p1 = new Point3D(4, 3, 2);
            var p2 = new Point3D(6, 7, 8);

            Vector3D v = p1 - p2;

            Assert.That(v.X, Is.EqualTo(-2).Within(Epsilon.Value));
            Assert.That(v.Y, Is.EqualTo(-4).Within(Epsilon.Value));
            Assert.That(v.Z, Is.EqualTo(-6).Within(Epsilon.Value));
        }

        [Test]
        public void Add_PointPlusVector_ProducesPoint()
        {
            var p1 = new Point3D(4, 3, 2);
            var v = new Vector3D(3, 2, 1);

            Point3D p2 = p1 + v;

            Assert.That(p2.X, Is.EqualTo(7).Within(Epsilon.Value));
            Assert.That(p2.Y, Is.EqualTo(5).Within(Epsilon.Value));
            Assert.That(p2.Z, Is.EqualTo(3).Within(Epsilon.Value));
        }

        [Test]
        public void Deconstruct_Point3D_ProducesExpectedResults()
        {
            var p = new Point3D(4, 3, 2);

            (var x, var y, var z) = p;

            Assert.That(x, Is.EqualTo(4).Within(Epsilon.Value));
            Assert.That(y, Is.EqualTo(3).Within(Epsilon.Value));
            Assert.That(z, Is.EqualTo(2).Within(Epsilon.Value));
        }
    }
}