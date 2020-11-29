using NUnit.Framework;

using Rayzin.Objects;
using Rayzin.Primitives;

namespace Rayzin.Tests.Objects
{
    [TestFixture]
    public class RzSphereTests
    {
        [Test]
        public void ARayIntersectsASphereAtTwoPoints()
        {
            var r = new RzRay(new RzPoint(0, 0, -5), new RzVector(0, 0, 1));
            var s = new RzSphere();

            RzIntersectionsCollection xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(2));
            Assert.That(xs[0].Time, Is.EqualTo(4));
            Assert.That(xs[0].Object, Is.SameAs(s));
            Assert.That(xs[1].Time, Is.EqualTo(6));
            Assert.That(xs[1].Object, Is.SameAs(s));

            Assert.That(xs.Hit(), Is.EqualTo(xs[0]));
        }

        [Test]
        public void ARayIntersectsASphereAtTangent()
        {
            var r = new RzRay(new RzPoint(0, 1, -5), new RzVector(0, 0, 1));
            var s = new RzSphere();

            RzIntersectionsCollection xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(1));
            Assert.That(xs[0].Time, Is.EqualTo(5));

            Assert.That(xs.Hit(), Is.EqualTo(xs[0]));
        }

        [Test]
        public void ARayMissesASphere()
        {
            var r = new RzRay(new RzPoint(0, 2, -5), new RzVector(0, 0, 1));
            var s = new RzSphere();

            RzIntersectionsCollection xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(0));

            Assert.That(xs.Hit(), Is.Null);
        }

        [Test]
        public void ARayOriginatesInsideASphere()
        {
            var r = new RzRay(new RzPoint(0, 0, 0), new RzVector(0, 0, 1));
            var s = new RzSphere();

            RzIntersectionsCollection xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(2));
            Assert.That(xs[0].Time, Is.EqualTo(-1));
            Assert.That(xs[1].Time, Is.EqualTo(1));

            Assert.That(xs.Hit(), Is.EqualTo(xs[1]));
        }

        [Test]
        public void ASphereIsBehindARay()
        {
            var r = new RzRay(new RzPoint(0, 0, 5), new RzVector(0, 0, 1));
            var s = new RzSphere();

            RzIntersectionsCollection xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(2));
            Assert.That(xs[0].Time, Is.EqualTo(-6));
            Assert.That(xs[1].Time, Is.EqualTo(-4));

            Assert.That(xs.Hit(), Is.Null);
        }

        [Test]
        public void IntersectingAScaledSphereWithARay()
        {
            var r = new RzRay(new RzPoint(0, 0, -5), new RzVector(0, 0, 1));
            var s = new RzSphere { Transformation = RzTransforms.Scaling(2, 2, 2) };
            RzIntersectionsCollection xs = r.Intersect(s);
            CollectionAssert.AreEqual(new[] { new RzIntersection(s, 3), new RzIntersection(s, 7) }, xs);
        }

        [Test]
        public void IntersectingATranslatedSphereWithRay()
        {
            var r = new RzRay(new RzPoint(0, 0, -5), new RzVector(0, 0, 1));
            var s = new RzSphere { Transformation = RzTransforms.Translation(5, 0, 0) };
            RzIntersectionsCollection xs = r.Intersect(s);
            CollectionAssert.IsEmpty(xs);
        }
    }
}