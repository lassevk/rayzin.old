using NUnit.Framework;

using Rayzin.Primitives;

namespace Rayzin.Tests.Primitives
{
    [TestFixture]
    public class RayTests
    {
        [Test]
        public void CreatingAndQueryingARay()
        {
            var origin = new Point3D(1, 2, 3);
            var direction = new Vector3D(4, 5, 6);
            var r = new RayF(origin, direction);
            Assert.That(r.Origin, Is.EqualTo(origin));
            Assert.That(r.Direction, Is.EqualTo(direction));
        }

        [Test]
        public void Position_ProducesExpectedResults()
        {
            var r = new RayF(new Point3D(2, 3, 4), new Vector3D(1, 0, 0));
            Assert.That(r.Position(0), Is.EqualTo(new Point3D(2, 3, 4)));
            Assert.That(r.Position(1), Is.EqualTo(new Point3D(3, 3, 4)));
            Assert.That(r.Position(-1), Is.EqualTo(new Point3D(1, 3, 4)));
            Assert.That(r.Position(2.5), Is.EqualTo(new Point3D(4.5, 3, 4)));
        }

        [Test]
        public void Transform_Translation_ProducesExpectedResults()
        {
            var r = new RayF(new Point3D(1, 2, 3), new Vector3D(0, 1, 0));
            MatrixF m = Transforms.Translation(3, 4, 5);
            RayF r2 = r.Transform(m);

            Assert.That(r2.Origin, Is.EqualTo(new Point3D(4, 6, 8)));
            Assert.That(r2.Direction, Is.EqualTo(new Vector3D(0, 1, 0)));
        }

        [Test]
        public void Transform_Scaling_ProducesExpectedResults()
        {
            var r = new RayF(new Point3D(1, 2, 3), new Vector3D(0, 1, 0));
            MatrixF m = Transforms.Scaling(2, 3, 4);
            RayF r2 = r.Transform(m);

            Assert.That(r2.Origin, Is.EqualTo(new Point3D(2, 6, 12)));
            Assert.That(r2.Direction, Is.EqualTo(new Vector3D(0, 3, 0)));
        }
    }
}
