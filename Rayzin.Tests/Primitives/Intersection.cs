using System;

using NUnit.Framework;

using Rayzin.Objects;
using Rayzin.Primitives;

// ReSharper disable ObjectCreationAsStatement

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class IntersectionTests
    {
        [Test]
        public void NullObject_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new Intersection(null, 0));
        }

        [Test]
        public void Properties_AreGivenExpectedValues()
        {
            var s = new Sphere();
            var i = new Intersection(s, 1);

            Assert.That(i.Object, Is.SameAs(s));
            Assert.That(i.Time, Is.EqualTo(1));
        }
    }
}
