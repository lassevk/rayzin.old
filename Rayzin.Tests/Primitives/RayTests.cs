using NUnit.Framework;

using Rayzin.Primitives;

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RayTests
    {
        [Test]
        public void CreatingAndQueryingARay()
        {
            var origin = new Point3D(1, 2, 3);
            var direction = new Vector3D(4, 5, 6);
            var r = new RayF(origin, direction);
            Assert.That(r.Origin, Is.EqualTo(origin));
            Assert.That(r.Direction, Is.EqualTo(direction));
        }

        [Test]
        public void Position_ProducesExpectedResults()
        {
            var r = new RayF(new Point3D(2, 3, 4), new Vector3D(1, 0, 0));
            Assert.That(r.Position(0), Is.EqualTo(new Point3D(2, 3, 4)));
            Assert.That(r.Position(1), Is.EqualTo(new Point3D(3, 3, 4)));
            Assert.That(r.Position(-1), Is.EqualTo(new Point3D(1, 3, 4)));
            Assert.That(r.Position(2.5), Is.EqualTo(new Point3D(4.5, 3, 4)));
        }
    }
}
