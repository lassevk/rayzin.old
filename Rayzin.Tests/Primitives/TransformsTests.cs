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
    }
}
