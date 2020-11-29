using NUnit.Framework;

using Rayzin.Objects.LightSources;
using Rayzin.Primitives;

namespace Rayzin.Tests.Objects.LightSources
{
    [TestFixture]
    public class RzPointLightTests
    {
        [Test]
        public void APointLightHasAPositionAndIntensity()
        {
            RzColor intensity = RzColor.Presets.White;
            RzPoint position = RzPoint.Presets.Origin;
            var light = new RzPointLight(position, intensity);

            Assert.That(light.Position, Is.EqualTo(position));
            Assert.That(light.Intensity, Is.EqualTo(intensity));
        }
    }
}
