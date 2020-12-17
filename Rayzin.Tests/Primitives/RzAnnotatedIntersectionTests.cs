using NUnit.Framework;

using Rayzin.Objects.Renderables;
using Rayzin.Primitives;

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RzAnnotatedIntersectionTests
    {
        [Test]
        public void TheHitWhenAnIntersectionOccursOnTheOutside()
        {
            var r = new RzRay((0, 0, -5), (0, 0, 1));
            var shape = new RzSphere();
            RzIntersection i = shape.Intersect(r).Hit()!.Value;
            
            var comps = new RzAnnotatedIntersection(i, r);

            Assert.That(comps.IsInside, Is.False);
        }

        [Test]
        public void TheHitWhenAnIntersectionOccursOnTheInside()
        {
            var r = new RzRay((0, 0, 0), (0, 0, 1));
            var shape = new RzSphere();
            RzIntersection i = shape.Intersect(r).Hit()!.Value;

            var comps = new RzAnnotatedIntersection(i, r);

            Assert.That(comps.Point, Is.EqualTo(new RzPoint(0, 0, 1)));
            Assert.That(comps.EyeVector, Is.EqualTo(new RzVector(0, 0, -1)));
            Assert.That(comps.IsInside, Is.True);
            Assert.That(comps.NormalVector, Is.EqualTo(new RzVector(0, 0, -1)));
        }
    }
}
