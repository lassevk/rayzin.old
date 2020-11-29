using System;

using NUnit.Framework;

using Rayzin.Primitives;

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RzTransformsTests
    {
        [Test]
        public void TranslatePoint_ReturnsCorrectPoint()
        {
            RzMatrix transform = RzTransforms.Translation(5, -3, 2);
            var point = new RzPoint(-3, 4, 5);

            RzPoint output = transform * point;
            Assert.That(output, Is.EqualTo(new RzPoint(2, 1, 7)));
        }

        [Test]
        public void TranslatePointWithInverse_ReturnsCorrectPoint()
        {
            RzMatrix transform = RzTransforms.Translation(5, -3, 2);
            RzMatrix inverse = transform.Inverse();
            var p = new RzPoint(-3, 4, 5);
            RzPoint output = inverse * p;

            Assert.That(output, Is.EqualTo(new RzPoint(-8, 7, 3)));
        }

        [Test]
        public void TranslateVector_ReturnsOriginalVector()
        {
            RzMatrix transform = RzTransforms.Translation(5, -3, 2);
            var v = new RzVector(-3, 4, 5);

            RzVector output = transform * v;
            Assert.That(output, Is.EqualTo(v));
        }

        [Test]
        public void ScalePoint_ReturnsExpectedResults()
        {
            RzMatrix transform = RzTransforms.Scaling(2, 3, 4);
            var p = new RzPoint(-4, 6, 8);
            RzPoint output = transform * p;
            Assert.That(output, Is.EqualTo(new RzPoint(-8, 18, 32)));
        }

        [Test]
        public void ScaleVector_ReturnsExpectedResults()
        {
            RzMatrix transform = RzTransforms.Scaling(2, 3, 4);
            var v = new RzVector(-4, 6, 8);
            RzVector output = transform * v;
            Assert.That(output, Is.EqualTo(new RzVector(-8, 18, 32)));
        }

        [Test]
        public void ScaleByInverse_ReturnsExpectedResults()
        {
            RzMatrix transform = RzTransforms.Scaling(2, 3, 4);
            RzMatrix inv = transform.Inverse();
            var v = new RzVector(-4, 6, 8);
            RzVector output = inv * v;
            Assert.That(output, Is.EqualTo(new RzVector(-2, 2, 2)));
        }

        [Test]
        public void ScaleReflection_ReturnsExpectedResults()
        {
            RzMatrix transform = RzTransforms.Scaling(-1, 1, 1);
            var p = new RzPoint(2, 3, 4);
            RzPoint output = transform * p;
            Assert.That(output, Is.EqualTo(new RzPoint(-2, 3, 4)));
        }

        [Test]
        public void RotationAroundX_ReturnsExpectedResults()
        {
            var p = new RzPoint(0, 1, 0);
            RzMatrix halfQuarter = RzTransforms.RotationX(Math.PI / 4);
            RzMatrix fullQuarter = RzTransforms.RotationX(Math.PI / 2);
            Assert.That(halfQuarter * p, Is.EqualTo(new RzPoint(0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2)));
            Assert.That(fullQuarter * p, Is.EqualTo(new RzPoint(0, 0, 1)));
        }

        [Test]
        public void InverseRotationAroundX_ReturnsExpectedResults()
        {
            var p = new RzPoint(0, 1, 0);
            RzMatrix halfQuarter = RzTransforms.RotationX(Math.PI / 4);
            RzMatrix inverse = halfQuarter.Inverse();
            Assert.That(inverse * p, Is.EqualTo(new RzPoint(0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2)));
        }

        [Test]
        public void RotationAroundY_ReturnsExpectedResults()
        {
            var p = new RzPoint(0, 0, 1);
            RzMatrix halfQuarter = RzTransforms.RotationY(Math.PI / 4);
            RzMatrix fullQuarter = RzTransforms.RotationY(Math.PI / 2);
            Assert.That(halfQuarter * p, Is.EqualTo(new RzPoint(Math.Sqrt(2) / 2, 0, Math.Sqrt(2) / 2)));
            Assert.That(fullQuarter * p, Is.EqualTo(new RzPoint(1, 0, 0)));
        }

        [Test]
        public void RotationAroundZ_ReturnsExpectedResults()
        {
            var p = new RzPoint(0, 1, 0);
            RzMatrix halfQuarter = RzTransforms.RotationZ(Math.PI / 4);
            RzMatrix fullQuarter = RzTransforms.RotationZ(Math.PI / 2);
            Assert.That(halfQuarter * p, Is.EqualTo(new RzPoint(-Math.Sqrt(2) / 2, Math.Sqrt(2) / 2, 0)));
            Assert.That(fullQuarter * p, Is.EqualTo(new RzPoint(-1, 0, 0)));
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
            RzMatrix transform = RzTransforms.Shearing(xToY, xToZ, yToX, yToZ, zToX, zToY);
            var p = new RzPoint(2, 3, 4);
            RzPoint output = transform * p;
            Assert.That(output, Is.EqualTo(new RzPoint(expectedX, expectedY, expectedZ)));
        }

        [Test]
        public void ApplyMultipleTranslationsInSequence_ProducesExpectedResults()
        {
            var p = new RzPoint(1, 0, 1);
            RzMatrix a = RzTransforms.RotationX(Math.PI / 2);
            RzMatrix b = RzTransforms.Scaling(5, 5, 5);
            RzMatrix c = RzTransforms.Translation(10, 5, 7);

            RzPoint p2 = a * p;
            Assert.That(p2, Is.EqualTo(new RzPoint(1, -1, 0)));

            RzPoint p3 = b * p2;
            Assert.That(p3, Is.EqualTo(new RzPoint(5, -5, 0)));

            RzPoint p4 = c * p3;
            Assert.That(p4, Is.EqualTo(new RzPoint(15, 0, 7)));
        }

        [Test]
        public void ApplyMultipleTranslationsAsOneOperation_ProducesExpectedResults()
        {
            var p = new RzPoint(1, 0, 1);
            RzMatrix a = RzTransforms.RotationX(Math.PI / 2);
            RzMatrix b = RzTransforms.Scaling(5, 5, 5);
            RzMatrix c = RzTransforms.Translation(10, 5, 7);

            RzMatrix t = c * b * a;
            Assert.That(t * p, Is.EqualTo(new RzPoint(15, 0, 7)));
        }

        [Test]
        public void ApplyMultipleTranslationsAsOneOperation_UsingFluentAPI_ProducesExpectedResults()
        {
            var p = new RzPoint(1, 0, 1);
            RzMatrix t = RzMatrix.Identity(4).RotateX(Math.PI / 2).Scale(5, 5, 5).Translate(10, 5, 7);
            Assert.That(t * p, Is.EqualTo(new RzPoint(15, 0, 7)));
        }
    }
}
