using System.Linq;

using NUnit.Framework;

using Rayzin.Materials;
using Rayzin.Objects;
using Rayzin.Objects.LightSources;
using Rayzin.Objects.Renderables;
using Rayzin.Primitives;

namespace Rayzin.Tests
{
    [TestFixture]
    public class RzWorldTests
    {
        [Test]
        public void TheDefaultWorld()
        {
            var world = new RzWorld();

            CollectionAssert.AreEqual(
                new[] { new RzPointLight((-10, 10, -10), RzColor.Presets.White) }, world.Objects.OfType<RzLightSource>());

            CollectionAssert.AreEquivalent(
                new RzObject[]
                {
                    new RzSphere
                    {
                        Material = new RzPhongMaterial { Ambient = 0.1, Color = (0.8, 1.0, 0.6), Diffuse = 0.7, Specular = 0.2 }
                    },
                    new RzSphere
                    {
                        Transformation = RzTransforms.None.Scale(0.5)
                    },
                }, world.Objects.OfType<RzRenderable>());
        }

        [Test]
        public void IntersectAWorldWithARay()
        {
            var w = new RzWorld();
            var r = new RzRay((0, 0, -5), (0, 0, 1));
            var xs = w.Intersect(r);

            CollectionAssert.AreEqual(new[] { 4, 4.5, 5.5, 6 }, xs.Select(s => s.Time));
        }
    }
}
