using System;

namespace Rayzin.Primitives
{
    public static class RzTransforms
    {
        public static readonly RzMatrix None = RzMatrix.Presets.Identity4;
        public static RzMatrix Translation(double x, double y, double z) => new RzMatrix(4, 1, 0, 0, x, 0, 1, 0, y, 0, 0, 1, z, 0, 0, 0, 1);
        public static RzMatrix Scaling(double x, double y, double z) => new RzMatrix(4, x, 0, 0, 0, 0, y, 0, 0, 0, 0, z, 0, 0, 0, 0, 1);

        public static RzMatrix RotationX(double radians)
        {
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);
            return new RzMatrix(4, 1, 0, 0, 0, 0, cos, -sin, 0, 0, sin, cos, 0, 0, 0, 0, 1);
        }

        public static RzMatrix RotationY(double radians)
        {
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);
            return new RzMatrix(4, cos, 0, sin, 0, 0, 1, 0, 0, -sin, 0, cos, 0, 0, 0, 0, 1);
        }

        public static RzMatrix RotationZ(double radians)
        {
            var cos = Math.Cos(radians);
            var sin = Math.Sin(radians);
            return new RzMatrix(4, cos, -sin, 0, 0, sin, cos, 0, 0, 0, 0, 1, 0, 0, 0, 0, 1);
        }

        public static RzMatrix Shearing(double xToY, double xToZ, double yToX, double yToZ, double zToX, double zToY) => new RzMatrix(4, 1, xToY, xToZ, 0, yToX, 1, yToZ, 0, zToX, zToY, 1, 0, 0, 0, 0, 1);
    }
}
