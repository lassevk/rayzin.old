using NUnit.Framework;
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Core.Tests
{
    [TestFixture]
    public class Matrix4FTests
    {
        [Test]
        public void Constructor_BasicExample_ProducesExpectedResults()
        {
            var m = new Matrix4F(1, 2, 3, 4, 5.5, 6.5, 7.5, 8.5, 9, 10, 11, 12, 13.5, 14.5, 15.5, 16.5);

            Assert.That(m[0, 0], Is.EqualTo(1).Within(1e-5));
            Assert.That(m[0, 3], Is.EqualTo(4).Within(1e-5));
            Assert.That(m[1, 0], Is.EqualTo(5.5).Within(1e-5));
            Assert.That(m[1, 2], Is.EqualTo(7.5).Within(1e-5));
            Assert.That(m[2, 2], Is.EqualTo(11).Within(1e-5));
            Assert.That(m[3, 0], Is.EqualTo(13.5).Within(1e-5));
            Assert.That(m[3, 2], Is.EqualTo(15.5).Within(1e-5));
        }
    }
}