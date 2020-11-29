using NUnit.Framework;

using Rayzin.Objects;
using Rayzin.Primitives;

namespace Rayzin.Tests.Objects
{
    [TestFixture]
    public class SphereTests
    {
        [Test]
        public void ARayIntersectsASphereAtTwoPoints()
        {
            var r = new RayF(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(2));
            Assert.That(xs[0].Time, Is.EqualTo(4));
            Assert.That(xs[0].Object, Is.SameAs(s));
            Assert.That(xs[1].Time, Is.EqualTo(6));
            Assert.That(xs[1].Object, Is.SameAs(s));
        }

        [Test]
        public void ARayIntersectsASphereAtTangent()
        {
            var r = new RayF(new Point3D(0, 1, -5), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(1));
            Assert.That(xs[0].Time, Is.EqualTo(5));
        }

        [Test]
        public void ARayMissesASphere()
        {
            var r = new RayF(new Point3D(0, 2, -5), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(0));
        }

        [Test]
        public void ARayOriginatesInsideASphere()
        {
            var r = new RayF(new Point3D(0, 0, 0), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(2));
            Assert.That(xs[0].Time, Is.EqualTo(-1));
            Assert.That(xs[1].Time, Is.EqualTo(1));
        }

        [Test]
        public void ASphereIsBehindARay()
        {
            var r = new RayF(new Point3D(0, 0, 5), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(2));
            Assert.That(xs[0].Time, Is.EqualTo(-6));
            Assert.That(xs[1].Time, Is.EqualTo(-4));
        }
    }
}
