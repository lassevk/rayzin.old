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

            Assert.That(xs.Hit(), Is.EqualTo(xs[0]));
        }

        [Test]
        public void ARayIntersectsASphereAtTangent()
        {
            var r = new RayF(new Point3D(0, 1, -5), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(1));
            Assert.That(xs[0].Time, Is.EqualTo(5));

            Assert.That(xs.Hit(), Is.EqualTo(xs[0]));
        }

        [Test]
        public void ARayMissesASphere()
        {
            var r = new RayF(new Point3D(0, 2, -5), new Vector3D(0, 0, 1));
            var s = new Sphere();

            Intersections xs = r.Intersect(s);
            Assert.That(xs.Count, Is.EqualTo(0));

            Assert.That(xs.Hit(), Is.Null);
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

            Assert.That(xs.Hit(), Is.EqualTo(xs[1]));
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

            Assert.That(xs.Hit(), Is.Null);
        }

        [Test]
        public void SphereDefaultTransformation()
        {
            var s = new Sphere();

            Assert.That(s.Transformation, Is.EqualTo(MatrixF.Presets.Identity4));
        }

        [Test]
        public void ChangingASpheresTransformation()
        {
            var s = new Sphere();
            MatrixF t = Transforms.Translation(2, 3, 4);

            s.Transformation = t;
            Assert.That(s.Transformation, Is.EqualTo(t));
        }

        [Test]
        public void IntersectingAScaledSphereWithARay()
        {
            var r = new RayF(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));
            var s = new Sphere { Transformation = Transforms.Scaling(2, 2, 2) };
            Intersections xs = r.Intersect(s);
            CollectionAssert.AreEqual(new[] { new Intersection(s, 3), new Intersection(s, 7) }, xs);
        }

        [Test]
        public void IntersectingATranslatedSphereWithRay()
        {
            var r = new RayF(new Point3D(0, 0, -5), new Vector3D(0, 0, 1));
            var s = new Sphere { Transformation = Transforms.Translation(5, 0, 0) };
            Intersections xs = r.Intersect(s);
            CollectionAssert.IsEmpty(xs);
        }
    }
}