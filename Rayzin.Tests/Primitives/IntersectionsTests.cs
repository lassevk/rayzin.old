using System;

using NUnit.Framework;

using Rayzin.Objects;
using Rayzin.Primitives;

// ReSharper disable ObjectCreationAsStatement

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class IntersectionsTests
    {
        [Test]
        public void NullIntersections_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Intersections(null));
        }

        [Test]
        public void Properties_AreGivenExpectedValues()
        {
            var i1 = new Intersection(new Sphere(), 1);
            var i2 = new Intersection(new Sphere(), 2);

            var i = new Intersections(i1, i2);

            Assert.That(i.Count, Is.EqualTo(2));
            Assert.That(i[0].Time, Is.EqualTo(1));
            Assert.That(i[1].Time, Is.EqualTo(2));
        }

        [Test]
        public void Hit_WithRandomOrder_ProducesCorrectResults()
        {
            var s = new Sphere();
            var i1 = new Intersection(s, 5);
            var i2 = new Intersection(s, 7);
            var i3 = new Intersection(s, -3);
            var i4 = new Intersection(s, 2);
            var i = new Intersections(i1, i2, i3, i4);

            Assert.That(i.Hit(), Is.EqualTo(i4));
        }
    }
}
