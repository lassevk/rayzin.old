using System.Drawing.Imaging;

using Rayzin.Objects.Renderables;
using Rayzin.Primitives;

namespace Rayzin.Sandbox
{
    class Program
    {
        static void Main()
        {
            var canvas = new RzCanvas(1024, 1024);

            RzPoint origin = (0, 0, -5);
            double wallZ = 10;
            double wallSize = 7;
            var pixelSize = wallSize / canvas.Width;
            var half = wallSize / 2;

            var shape = new RzSphere();
            shape.Transformation = RzTransforms.None.Translate(1, 0, 0);
            for (var y = 0; y < canvas.Height; y++)
            {
                var worldY = half - pixelSize * y;
                for (var x = 0; x < canvas.Width; x++)
                {
                    var worldX = -half + pixelSize * x;
                    var position = new RzPoint(worldX, worldY, wallZ);
                    var r = new RzRay(origin, (position - origin).Normalize());
                    var xs = shape.Intersect(r);
                    if (xs.Hit().HasValue)
                        canvas[x, y] = RzColor.Presets.Red;
                }
            }

            canvas.ToBitmap().Save(@"D:\Temp\test.png", ImageFormat.Png);
        }

        private static Projectile Tick(Environment env, Projectile proj)
        {
            RzPoint position = proj.Position + proj.Velocity;
            RzVector velocity = proj.Velocity + env.Gravity + env.Wind;
            return new Projectile(position, velocity);
        }
    }
}