using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class Matrix4Tests
    {
        [Test]
        public void Constructor_WithValues_PutsAllTheValuesIntoTheRightPlaces()
        {
            var m = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.That(m.M00, Is.EqualTo(1));
            Assert.That(m.M01, Is.EqualTo(2));
            Assert.That(m.M02, Is.EqualTo(3));
            Assert.That(m.M03, Is.EqualTo(4));

            Assert.That(m.M10, Is.EqualTo(5));
            Assert.That(m.M11, Is.EqualTo(6));
            Assert.That(m.M12, Is.EqualTo(7));
            Assert.That(m.M13, Is.EqualTo(8));

            Assert.That(m.M20, Is.EqualTo(9));
            Assert.That(m.M21, Is.EqualTo(10));
            Assert.That(m.M22, Is.EqualTo(11));
            Assert.That(m.M23, Is.EqualTo(12));
        
            Assert.That(m.M30, Is.EqualTo(13));
            Assert.That(m.M31, Is.EqualTo(14));
            Assert.That(m.M32, Is.EqualTo(15));
            Assert.That(m.M33, Is.EqualTo(16));
        }

        [Test]
        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 2)]
        [TestCase(0, 2, 3)]
        [TestCase(0, 3, 4)]
        [TestCase(1, 0, 5)]
        [TestCase(1, 1, 6)]
        [TestCase(1, 2, 7)]
        [TestCase(1, 3, 8)]
        [TestCase(2, 0, 9)]
        [TestCase(2, 1, 10)]
        [TestCase(2, 2, 11)]
        [TestCase(2, 3, 12)]
        [TestCase(3, 0, 13)]
        [TestCase(3, 1, 14)]
        [TestCase(3, 2, 15)]
        [TestCase(3, 3, 16)]
        public void Indexer_ForAllElements_ReturnsTheCorrectValue(int row, int column, double expected)
        {
            var m = new Matrix4(1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            Assert.That(m[row, column], Is.EqualTo(expected).Within(Epsilon.Value));
        }
    }
}