using System;

using NUnit.Framework;

using Rayzin.Materials;
using Rayzin.Objects.LightSources;
using Rayzin.Primitives;

namespace Rayzin.Tests.Materials
{
    [TestFixture]
    public class RzPhongMaterialTests
    {
        [Test]
        public void DefaultValues()
        {
            var m = new RzPhongMaterial();

            Assert.That(m.Color, Is.EqualTo(RzColor.Presets.White));
            Assert.That(m.Ambient, Is.EqualTo(0.1));
            Assert.That(m.Diffuse, Is.EqualTo(0.9));
            Assert.That(m.Specular, Is.EqualTo(0.9));
            Assert.That(m.Shininess, Is.EqualTo(200.0));
        }

        [Test]
        public void LightingWithTheEyeBetweenTheLightAndTheSurface()
        {
            RzPoint position = RzPoint.Presets.Origin;
            RzVector eyeVector = (0, 0, -1);
            RzVector normalVector = (0, 0, -1);
            RzPointLight light = new((0, 0, -10), RzColor.Presets.White);
            RzPhongMaterial material = new();

            RzColor result = material.Lighting(light, position, eyeVector, normalVector);

            Assert.That(result, Is.EqualTo(new RzColor(1.9, 1.9, 1.9)));
        }

        [Test]
        public void LightingWithTheEyeBetweenLightAndSurface_EyeOffset45Degrees()
        {
            RzPoint position = RzPoint.Presets.Origin;
            RzVector eyeVector = (0, Math.Sqrt(2) / 2, Math.Sqrt(2) / 2);
            RzVector normalVector = (0, 0, -1);
            RzPointLight light = new((0, 0, -10), RzColor.Presets.White);
            RzPhongMaterial material = new();

            RzColor result = material.Lighting(light, position, eyeVector, normalVector);

            Assert.That(result, Is.EqualTo(new RzColor(1.0, 1.0, 1.0)));
        }

        [Test]
        public void LightingWithEyeOppositeSurface_LightOffset45Degrees()
        {
            RzPoint position = RzPoint.Presets.Origin;
            RzVector eyeVector = (0, 0, -1);
            RzVector normalVector = (0, 0, -1);
            RzPointLight light = new((0, 10, -10), RzColor.Presets.White);
            RzPhongMaterial material = new();

            RzColor result = material.Lighting(light, position, eyeVector, normalVector);

            Assert.That(result, Is.EqualTo(new RzColor(0.7364, 0.7364, 0.7364)));
        }

        [Test]
        public void LightingWithEyeInThePathOfTheReflectionVector()
        {
            RzPoint position = RzPoint.Presets.Origin;
            RzVector eyeVector = (0, -Math.Sqrt(2) / 2, -Math.Sqrt(2) / 2);
            RzVector normalVector = (0, 0, -1);
            RzPointLight light = new((0, 10, -10), RzColor.Presets.White);
            RzPhongMaterial material = new();

            RzColor result = material.Lighting(light, position, eyeVector, normalVector);

            Assert.That(result, Is.EqualTo(new RzColor(1.6364, 1.6364, 1.6364)));
        }

        [Test]
        public void LightingWithTheLightBehindTheSurface()
        {
            RzPoint position = RzPoint.Presets.Origin;
            RzVector eyeVector = (0, 0, -1);
            RzVector normalVector = (0, 0, -1);
            RzPointLight light = new((0, 0, 10), RzColor.Presets.White);
            RzPhongMaterial material = new();

            RzColor result = material.Lighting(light, position, eyeVector, normalVector);

            Assert.That(result, Is.EqualTo(new RzColor(0.1, 0.1, 0.1)));
        }
    }
}
