using System;

using NUnit.Framework;

using Rayzin.Objects.Renderables;
using Rayzin.Primitives;

// ReSharper disable ObjectCreationAsStatement

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RzIntersectionsTests
    {
        [Test]
        public void NullIntersections_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new RzIntersectionsCollection(null));
        }

        [Test]
        public void Properties_AreGivenExpectedValues()
        {
            var i1 = new RzIntersection(new RzSphere(), 1);
            var i2 = new RzIntersection(new RzSphere(), 2);

            var i = new RzIntersectionsCollection(i1, i2);

            Assert.That(i.Count, Is.EqualTo(2));
            Assert.That(i[0].Time, Is.EqualTo(1));
            Assert.That(i[1].Time, Is.EqualTo(2));
        }

        [Test]
        public void Hit_WithRandomOrder_ProducesCorrectResults()
        {
            var s = new RzSphere();
            var i1 = new RzIntersection(s, 5);
            var i2 = new RzIntersection(s, 7);
            var i3 = new RzIntersection(s, -3);
            var i4 = new RzIntersection(s, 2);
            var i = new RzIntersectionsCollection(i1, i2, i3, i4);

            Assert.That(i.Hit(), Is.EqualTo(i4));
        }
    }
}
