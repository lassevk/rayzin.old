using NUnit.Framework;
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Core.Tests
{
    [TestFixture]
    public class ColorFTests
    {
        [Test]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.2, 0.6, 0.7, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.3, 0.7, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.3, false)]
        [TestCase(0.5, 0.6, 0.7, 0.5+Epsilon.Value/2, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5-Epsilon.Value/2, 0.6, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6+Epsilon.Value/2, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6-Epsilon.Value/2, 0.7, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7+Epsilon.Value/2, true)]
        [TestCase(0.5, 0.6, 0.7, 0.5, 0.6, 0.7-Epsilon.Value/2, true)]
        public void Equals_WithTestCases_ReturnsCorrectValue(double r1, double g1, double b1, double r2, double g2, double b2, bool expected)
        {
            ColorF c1 = new ColorF(r1, g1, b1);
            ColorF c2 = new ColorF(r2, g2, b2);

            bool output = c1.Equals(c2);

            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void Add_TwoColors_ProducesCorrectValues()
        {
            var c1 = new ColorF(0.9, 0.6, 0.75);
            var c2 = new ColorF(0.7, 0.1, 0.25);

            ColorF c3 = c1 + c2;

            Assert.That(c3.Red, Is.EqualTo(1.6).Within(Epsilon.Value));
            Assert.That(c3.Green, Is.EqualTo(0.7).Within(Epsilon.Value));
            Assert.That(c3.Blue, Is.EqualTo(1.0).Within(Epsilon.Value));
        }
        
        [Test]
        public void Subtract_TwoColors_ProducesCorrectValues()
        {
            var c1 = new ColorF(0.9, 0.6, 0.75);
            var c2 = new ColorF(0.7, 0.1, 0.25);

            ColorF c3 = c1 - c2;

            Assert.That(c3.Red, Is.EqualTo(0.2).Within(Epsilon.Value));
            Assert.That(c3.Green, Is.EqualTo(0.5).Within(Epsilon.Value));
            Assert.That(c3.Blue, Is.EqualTo(0.5).Within(Epsilon.Value));
        }
        
        [Test]
        public void Multiply_ColorByScalar_ProducesCorrectValues()
        {
            var c1 = new ColorF(0.2, 0.3, 0.4);

            ColorF c3 = c1 * 2;

            Assert.That(c3.Red, Is.EqualTo(0.4).Within(Epsilon.Value));
            Assert.That(c3.Green, Is.EqualTo(0.6).Within(Epsilon.Value));
            Assert.That(c3.Blue, Is.EqualTo(0.8).Within(Epsilon.Value));
        }    

        [Test]
        public void Multiply_TwoColors_ProducesCorrectValues()
        {
            var c1 = new ColorF(1, 0.2, 0.4);
            var c2 = new ColorF(0.9, 1, 0.1);

            ColorF c3 = c1 * c2;

            Assert.That(c3.Red, Is.EqualTo(0.9).Within(Epsilon.Value));
            Assert.That(c3.Green, Is.EqualTo(0.2).Within(Epsilon.Value));
            Assert.That(c3.Blue, Is.EqualTo(0.04).Within(Epsilon.Value));
        }    
    }
}