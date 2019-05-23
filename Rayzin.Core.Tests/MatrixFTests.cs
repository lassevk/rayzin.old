using NUnit.Framework;
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Core.Tests
{
    [TestFixture]
    public class MatrixFTests
    {
        [Test]
        public void Constructor_4x4_ProducesExpectedResults()
        {
            var m = new MatrixF(4, new double[] { 1, 2, 3, 4, 5.5, 6.5, 7.5, 8.5, 9, 10, 11, 12, 13.5, 14.5, 15.5, 16.5 });

            Assert.That(m[0, 0], Is.EqualTo(1).Within(1e-5));
            Assert.That(m[0, 3], Is.EqualTo(4).Within(1e-5));
            Assert.That(m[1, 0], Is.EqualTo(5.5).Within(1e-5));
            Assert.That(m[1, 2], Is.EqualTo(7.5).Within(1e-5));
            Assert.That(m[2, 2], Is.EqualTo(11).Within(1e-5));
            Assert.That(m[3, 0], Is.EqualTo(13.5).Within(1e-5));
            Assert.That(m[3, 2], Is.EqualTo(15.5).Within(1e-5));
        }

        [Test]
        public void Constructor_2x2_ProducesExpectedResults()
        {
            var m = new MatrixF(2, new double[] { -3, 5, 1, -2 });

            Assert.That(m[0, 0], Is.EqualTo(-3).Within(1e-5));
            Assert.That(m[0, 1], Is.EqualTo(5).Within(1e-5));
            Assert.That(m[1, 0], Is.EqualTo(1).Within(1e-5));
            Assert.That(m[1, 1], Is.EqualTo(-2).Within(1e-5));
        }

        [Test]
        public void Constructor_3x3_ProducesExpectedResults()
        {
            var m = new MatrixF(3, new double[] { -3, 5, 0, 1, -2, -7, 0, 1, 1 });

            Assert.That(m[0, 0], Is.EqualTo(-3).Within(1e-5));
            Assert.That(m[1, 1], Is.EqualTo(-2).Within(1e-5));
            Assert.That(m[2, 2], Is.EqualTo(1).Within(1e-5));
        }
    }
}