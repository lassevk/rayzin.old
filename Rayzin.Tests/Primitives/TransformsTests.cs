using System;

using NUnit.Framework;

using Rayzin.Primitives;

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class TransformsTests
    {
        [Test]
        public void TranslatePoint_ReturnsCorrectPoint()
        {
            MatrixF transform = Transforms.Translation(5, -3, 2);
            var point = new Point3D(-3, 4, 5);

            Point3D output = transform * point;
            Assert.That(output, Is.EqualTo(new Point3D(2, 1, 7)));
        }

        [Test]
        public void TranslatePointWithInverse_ReturnsCorrectPoint()
        {
            MatrixF transform = Transforms.Translation(5, -3, 2);
            MatrixF inverse = transform.Inverse();
            var p = new Point3D(-3, 4, 5);
            Point3D output = inverse * p;

            Assert.That(output, Is.EqualTo(new Point3D(-8, 7, 3)));
        }

        [Test]
        public void TranslateVector_ReturnsOriginalVector()
        {
            MatrixF transform = Transforms.Translation(5, -3, 2);
            var v = new Vector3D(-3, 4, 5);

            Vector3D output = transform * v;
            Assert.That(output, Is.EqualTo(v));
        }

        [Test]
        public void ScalePoint_ReturnsExpectedResults()
        {
            MatrixF transform = Transforms.Scaling(2, 3, 4);
            var p = new Point3D(-4, 6, 8);
            Point3D output = transform * p;
            Assert.That(output, Is.EqualTo(new Point3D(-8, 18, 32)));
        }

        [Test]
        public void ScaleVector_ReturnsExpectedResults()
        {
            MatrixF transform = Transforms.Scaling(2, 3, 4);
            var v = new Vector3D(-4, 6, 8);
            Vector3D output = transform * v;
            Assert.That(output, Is.EqualTo(new Vector3D(-8, 18, 32)));
        }

        [Test]
        public void ScaleByInverse_ReturnsExpectedResults()
        {
            MatrixF transform = Transforms.Scaling(2, 3, 4);
            MatrixF inv = transform.Inverse();
            var v = new Vector3D(-4, 6, 8);
            Vector3D output = inv * v;
            Assert.That(output, Is.EqualTo(new Vector3D(-2, 2, 2)));
        }

        [Test]
        public void ScaleReflection_ReturnsExpectedResults()
        {
            MatrixF transform = Transforms.Scaling(-1, 1, 1);
            var p = new Point3D(2, 3, 4);
            Point3D output = transform * p;
            Assert.That(output, Is.EqualTo(new Point3D(-2, 3, 4)));
        }

        [Test]
        public void RotationAroundX_ReturnsExpectedResults()
        {
            var p = new Point3D(0, 1, 0);
            MatrixF halfQuarter = Transforms.RotationX(Math.PI / 4);
            MatrixF fullQuarter = Transforms.RotationX(Math.PI / 2);
            Assert.That(halfQuarter * p, Is.EqualTo(new Point3D(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2)));
            Assert.That(fullQuarter * p, Is.EqualTo(new Point3D(0, 0, 1)));
        }

        [Test]
        public void InverseRotationAroundX_ReturnsExpectedResults()
        {
            var p = new Point3D(0, 1, 0);
            MatrixF halfQuarter = Transforms.RotationX(Math.PI / 4);
            MatrixF inverse = halfQuarter.Inverse();
            Assert.That(inverse * p, Is.EqualTo(new Point3D(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2)));
        }

        [Test]
        public void RotationAroundY_ReturnsExpectedResults()
        {
            var p = new Point3D(0, 0, 1);
            MatrixF halfQuarter = Transforms.RotationY(Math.PI / 4);
            MatrixF fullQuarter = Transforms.RotationY(Math.PI / 2);
            Assert.That(halfQuarter * p, Is.EqualTo(new Point3D(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2)));
            Assert.That(fullQuarter * p, Is.EqualTo(new Point3D(1, 0, 0)));
        }

        [Test]
        public void RotationAroundZ_ReturnsExpectedResults()
        {
            var p = new Point3D(0, 1, 0);
            MatrixF halfQuarter = Transforms.RotationZ(Math.PI / 4);
            MatrixF fullQuarter = Transforms.RotationZ(Math.PI / 2);
            Assert.That(halfQuarter * p, Is.EqualTo(new Point3D(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0)));
            Assert.That(fullQuarter * p, Is.EqualTo(new Point3D(-1, 0, 0)));
        }

        [Test]
        [TestCase(1, 0, 0, 0, 0, 0, 5, 3, 4)]
        [TestCase(0, 1, 0, 0, 0, 0, 6, 3, 4)]
        [TestCase(0, 0, 1, 0, 0, 0, 2, 5, 4)]
        [TestCase(0, 0, 0, 1, 0, 0, 2, 7, 4)]
        [TestCase(0, 0, 0, 0, 1, 0, 2, 3, 6)]
        [TestCase(0, 0, 0, 0, 0, 1, 2, 3, 7)]
        public void ShearingTransform_WithTestCases_ProducesExpectedResults(double xToY, double xToZ, double yToX, double yToZ, double zToX, double zToY, double expectedX, double expectedY, double expectedZ)
        {
            MatrixF transform = Transforms.Shearing(xToY, xToZ, yToX, yToZ, zToX, zToY);
            var p = new Point3D(2, 3, 4);
            Point3D output = transform * p;
            Assert.That(output, Is.EqualTo(new Point3D(expectedX, expectedY, expectedZ)));
        }
    }
}
