using NUnit.Framework;
// ReSharper disable PossibleNullReferenceException

namespace Rayzin.Core.Tests
{
    [TestFixture]
    public class ColorFTests
    {
        [Test]
        public void Add_TwoColors_ProducesCorrectValues()
        {
            var c1 = new ColorF(0.9, 0.6, 0.75);
            var c2 = new ColorF(0.7, 0.1, 0.25);

            ColorF c3 = c1 + c2;

            Assert.That(c3.Red, Is.EqualTo(1.6).Within(1e-5));
            Assert.That(c3.Green, Is.EqualTo(0.7).Within(1e-5));
            Assert.That(c3.Blue, Is.EqualTo(1.0).Within(1e-5));
        }
        
        [Test]
        public void Subtract_TwoColors_ProducesCorrectValues()
        {
            var c1 = new ColorF(0.9, 0.6, 0.75);
            var c2 = new ColorF(0.7, 0.1, 0.25);

            ColorF c3 = c1 - c2;

            Assert.That(c3.Red, Is.EqualTo(0.2).Within(1e-5));
            Assert.That(c3.Green, Is.EqualTo(0.5).Within(1e-5));
            Assert.That(c3.Blue, Is.EqualTo(0.5).Within(1e-5));
        }
        
        [Test]
        public void Multiply_ColorByScalar_ProducesCorrectValues()
        {
            var c1 = new ColorF(0.2, 0.3, 0.4);

            ColorF c3 = c1 * 2;

            Assert.That(c3.Red, Is.EqualTo(0.4).Within(1e-5));
            Assert.That(c3.Green, Is.EqualTo(0.6).Within(1e-5));
            Assert.That(c3.Blue, Is.EqualTo(0.8).Within(1e-5));
        }    

        [Test]
        public void Multiply_TwoColors_ProducesCorrectValues()
        {
            var c1 = new ColorF(1, 0.2, 0.4);
            var c2 = new ColorF(0.9, 1, 0.1);

            ColorF c3 = c1 * c2;

            Assert.That(c3.Red, Is.EqualTo(0.9).Within(1e-5));
            Assert.That(c3.Green, Is.EqualTo(0.2).Within(1e-5));
            Assert.That(c3.Blue, Is.EqualTo(0.04).Within(1e-5));
        }    
    }
}