using NUnit.Framework;

using Rayzin.Materials;
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
    }
}
