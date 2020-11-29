using NUnit.Framework;

using Rayzin.Primitives;

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RzRayTests
    {
        [Test]
        public void CreatingAndQueryingARay()
        {
            var origin = new RzPoint(1, 2, 3);
            var direction = new RzVector(4, 5, 6);
            var r = new RzRay(origin, direction);
            Assert.That(r.Origin, Is.EqualTo(origin));
            Assert.That(r.Direction, Is.EqualTo(direction));
        }

        [Test]
        public void Position_ProducesExpectedResults()
        {
            var r = new RzRay(new RzPoint(2, 3, 4), new RzVector(1, 0, 0));
            Assert.That(r.Position(0), Is.EqualTo(new RzPoint(2, 3, 4)));
            Assert.That(r.Position(1), Is.EqualTo(new RzPoint(3, 3, 4)));
            Assert.That(r.Position(-1), Is.EqualTo(new RzPoint(1, 3, 4)));
            Assert.That(r.Position(2.5), Is.EqualTo(new RzPoint(4.5, 3, 4)));
        }

        [Test]
        public void Transform_Translation_ProducesExpectedResults()
        {
            var r = new RzRay(new RzPoint(1, 2, 3), new RzVector(0, 1, 0));
            RzMatrix m = RzTransforms.Translation(3, 4, 5);
            RzRay r2 = r.Transform(m);

            Assert.That(r2.Origin, Is.EqualTo(new RzPoint(4, 6, 8)));
            Assert.That(r2.Direction, Is.EqualTo(new RzVector(0, 1, 0)));
        }

        [Test]
        public void Transform_Scaling_ProducesExpectedResults()
        {
            var r = new RzRay(new RzPoint(1, 2, 3), new RzVector(0, 1, 0));
            RzMatrix m = RzTransforms.Scaling(2, 3, 4);
            RzRay r2 = r.Transform(m);

            Assert.That(r2.Origin, Is.EqualTo(new RzPoint(2, 6, 12)));
            Assert.That(r2.Direction, Is.EqualTo(new RzVector(0, 3, 0)));
        }
    }
}
