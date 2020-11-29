using System;
using System.Drawing.Imaging;

using Rayzin.Primitives;

namespace Rayzin.Sandbox
{
    class Program
    {
        static void Main()
        {
            var canvas = new CanvasF(1024, 1024);

            var point = new Point3D(512, 40, 0);
            MatrixF transform = MatrixF.Identity(4).Translate(-512, -512, 0).RotateZ(Math.PI * 2 / 12).Translate(512, 512, 0);

            for (var index = 0; index < 12; index++)
            {
                for (var dx = -10; dx <= 10; dx++)
                    for (var dy = -10; dy <= 10; dy++)
                        canvas[(int)(point.X + dx), (int)(point.Y + dy)] = ColorF.Presets.White;

                point = transform * point;
            }

            canvas.ToBitmap().Save(@"D:\Temp\test.png", ImageFormat.Png);
        }

        private static Projectile Tick(Environment env, Projectile proj)
        {
            Point3D position = proj.Position + proj.Velocity;
            Vector3D velocity = proj.Velocity + env.Gravity + env.Wind;
            return new Projectile(position, velocity);
        }
    }
}