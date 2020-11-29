using NUnit.Framework;

using Rayzin.Objects;
using Rayzin.Primitives;

namespace Rayzin.Tests.Objects
{
    [TestFixture]
    public class RzObjectTests
    {
        [Test]
        public void ObjectDefaultTransformation()
        {
            var s = new RzSphere();

            Assert.That(s.Transformation, Is.EqualTo(RzMatrix.Presets.Identity4));
        }

        [Test]
        public void ChangingAnObjectsTransformation()
        {
            var s = new RzSphere();
            RzMatrix t = RzTransforms.Translation(2, 3, 4);

            s.Transformation = t;
            Assert.That(s.Transformation, Is.EqualTo(t));
        }

        [Test]
        public void ChangingAnObjectsTransformationAlsoChangesInverseTransformation()
        {
            var s = new RzSphere();
            RzMatrix t = RzTransforms.Translation(2, 3, 4);

            s.Transformation = t;
            Assert.That(s.InverseTransformation, Is.EqualTo(t.Inverse()));
        }
    }
}