using System;
using System.Linq;

using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable ObjectCreationAsStatement

namespace Rayzin.Tests
{
    [TestFixture]
    public class RzCanvasTests
    {
        [Test]
        public void Constructor_ZeroWidth_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RzCanvas(0, 10));
        }

        [Test]
        public void Constructor_ZeroHeight_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new RzCanvas(10, 0));
        }

        [Test]
        public void Constructor_SetsCorrectWidthAndHeight()
        {
            var canvas = new RzCanvas(640, 480);

            Assert.That(canvas.Width, Is.EqualTo(640));
            Assert.That(canvas.Height, Is.EqualTo(480));
        }

        [Test]
        public void Pixels_RetainTheirColor()
        {
            var canvas = new RzCanvas(10, 20);
            var red = new RzColor(1, 0, 0);

            canvas[2, 3] = red;
            RzColor output = canvas[2, 3];

            Assert.That(output, Is.EqualTo(red));
        }

        [Test]
        public void ToPpm_HeaderOnly_IsCorrectlyProduced()
        {
            var canvas = new RzCanvas(5, 3);

            var ppm = canvas.ToPpm();

            var lines = ppm.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.AreEqual(new[] { "P3", "5 3", "255" }, lines.Take(3));
        }

        [Test]
        public void ToPpm_FullCanvas_IsCorrectlyProduced()
        {
            var canvas = new RzCanvas(5, 3);
            var c1 = new RzColor(1.5, 0, 0);
            var c2 = new RzColor(0, 0.5, 0);
            var c3 = new RzColor(-0.5, 0, 1);
            canvas[0, 0] = c1;
            canvas[2, 1] = c2;
            canvas[4, 2] = c3;

            var ppm = canvas.ToPpm();

            var lines = ppm.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.AreEqual(
                new[]
                {
                    "P3", "5 3", "255", "255 0 0 0 0 0 0 0 0 0 0 0 0 0 0", "0 0 0 0 0 0 0 128 0 0 0 0 0 0 0",
                    "0 0 0 0 0 0 0 0 0 0 0 0 0 0 255"
                }, lines);
        }

        [Test]
        public void ToPpm_LongLines_IsCorrectlyProduced()
        {
            var canvas = new RzCanvas(10, 2);
            var color = new RzColor(1, 0.8, 0.6);
            canvas.Clear(color);

            var ppm = canvas.ToPpm();

            var lines = ppm.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.AreEqual(
                new[]
                {
                    "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204",
                    "153 255 204 153 255 204 153 255 204 153 255 204 153",
                    "255 204 153 255 204 153 255 204 153 255 204 153 255 204 153 255 204",
                    "153 255 204 153 255 204 153 255 204 153 255 204 153"
                }, lines.Skip(3));
        }
    }
}