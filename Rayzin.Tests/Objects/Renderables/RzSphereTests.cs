using System;

using NUnit.Framework;

using Rayzin.Objects.Renderables;
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

        [Test]
        [TestCase(1, 0, 0, 1, 0, 0, TestName = "On X axis")]
        [TestCase(0, 1, 0, 0, 1, 0, TestName = "On Y axis")]
        [TestCase(0, 0, 1, 0, 0, 1, TestName = "On Z axis")]
        [TestCase(0.577350269189627D, 0.577350269189627D, 0.577350269189627D, 0.577350269189627D, 0.577350269189627D, 0.577350269189627D, TestName = "On nonaxial point")]
        [TestCase(2, 2, 2, 0.577350269189625D, 0.577350269189625D, 0.577350269189625D, TestName = "Normal vector is always normalized")]
        public void NormalOnASphere_WithTestCases_ProducesExpectedResults(double x, double y, double z, double expectedX, double expectedY, double expectedZ)
        {
            var s = new RzSphere();
            RzVector n = s.NormalAt((x, y, z));
            
            RzVector expected = (expectedX, expectedY, expectedZ);

            Assert.That(n, Is.EqualTo(expected));
        }

        [Test]
        public void ComputingTheNormalOnATranslatedSphere()
        {
            var s = new RzSphere { Transformation = RzTransforms.Translation(0, 1, 0) };
            RzVector n = s.NormalAt((0, 1.70711, -0.70711));
            RzVector expected = (0, 0.70711, -0.70711);
            Assert.That(n, Is.EqualTo(expected));
        }

        [Test]
        public void ComputingTheNormalOnATransformedSphere()
        {
            var s = new RzSphere { Transformation = RzTransforms.Scaling(1, 0.5, 1) * RzTransforms.RotationZ(Math.PI / 5) };
            RzVector n = s.NormalAt((0, Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2));
            RzVector expected = (0, 0.97014, -0.24254);
            Assert.That(n, Is.EqualTo(expected));
        }
    }
}