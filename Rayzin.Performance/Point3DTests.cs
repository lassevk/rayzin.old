

// ReSharper disable ObjectCreationAsStatement

using Rayzin.Primitives;

namespace Rayzin.Performance
{
    internal class Point3DTests
    {
        public void Run()
        {
            Constructor();
            CompareEqual();
            CompareDifferent();
        }

        private void CompareEqual()
        {
            Point3D p1 = new Point3D(1, 1, 1);
            Point3D p2 = new Point3D(1, 1, 1);

            for (int index = 0; index < 100000; index++)
                p1.Equals(p2);
        }

        private void CompareDifferent()
        {
            Point3D p1 = new Point3D(1, 1, 1);
            Point3D p2 = new Point3D(2, 2, 2);

            for (int index = 0; index < 100000; index++)
                p1.Equals(p2);
        }

        private void Constructor()
        {
            for (int index = 0; index < 100000; index++)
                new Point3D(1, 1, 1);
        }
    }
}