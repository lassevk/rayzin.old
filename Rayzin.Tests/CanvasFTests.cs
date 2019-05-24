using System;
using System.Linq;

using NUnit.Framework;

using Rayzin.Primitives;

// ReSharper disable ObjectCreationAsStatement

namespace Rayzin.Tests
{
    [TestFixture]
    public class CanvasFTests
    {
        [Test]
        public void Constructor_ZeroWidth_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CanvasF(0, 10));
        }

        [Test]
        public void Constructor_ZeroHeight_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new CanvasF(10, 0));
        }

        [Test]
        public void Constructor_SetsCorrectWidthAndHeight()
        {
            var canvas = new CanvasF(640, 480);

            Assert.That(canvas.Width, Is.EqualTo(640));
            Assert.That(canvas.Height, Is.EqualTo(480));
        }

        [Test]
        public void Pixels_RetainTheirColor()
        {
            var canvas = new CanvasF(10, 20);
            var red = new ColorF(1, 0, 0);

            canvas[2, 3] = red;
            ColorF output = canvas[2, 3];

            Assert.That(output, Is.EqualTo(red));
        }

        [Test]
        public void ToPpm_HeaderOnly_IsCorrectlyProduced()
        {
            var canvas = new CanvasF(5, 3);

            var ppm = canvas.ToPpm();

            var lines = ppm.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.AreEqual(new[] { "P3", "5 3", "255" }, lines.Take(3));
        }

        [Test]
        public void ToPpm_FullCanvas_IsCorrectlyProduced()
        {
            var canvas = new CanvasF(5, 3);
            var c1 = new ColorF(1.5, 0, 0);
            var c2 = new ColorF(0, 0.5, 0);
            var c3 = new ColorF(-0.5, 0, 1);
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
            var canvas = new CanvasF(10, 2);
            var color = new ColorF(1, 0.8, 0.6);
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