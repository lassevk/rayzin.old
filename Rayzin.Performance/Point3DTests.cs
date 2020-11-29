

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
            RzPoint p1 = new RzPoint(1, 1, 1);
            RzPoint p2 = new RzPoint(1, 1, 1);

            for (int index = 0; index < 100000; index++)
                p1.Equals(p2);
        }

        private void CompareDifferent()
        {
            RzPoint p1 = new RzPoint(1, 1, 1);
            RzPoint p2 = new RzPoint(2, 2, 2);

            for (int index = 0; index < 100000; index++)
                p1.Equals(p2);
        }

        private void Constructor()
        {
            for (int index = 0; index < 100000; index++)
                new RzPoint(1, 1, 1);
        }
    }
}