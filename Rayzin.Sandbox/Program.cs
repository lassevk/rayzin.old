using System.Drawing.Imaging;

using Rayzin.Primitives;

namespace Rayzin.Sandbox
{
    class Program
    {
        static void Main()
        {
            var canvas = new CanvasF(900, 550);
            canvas.Clear(ColorF.Presets.Blue);
            var env = new Environment(new Vector3D(0, -0.1, 0), new Vector3D(-0.01, 0, 0));
            var p = new Projectile(new Point3D(0, 0, 0), new Vector3D(1, 1.8, 0).Normalize() * 11.25);

            while (p.Position.Y > 0)
            {
                p = Tick(env, p);

                int x = (int)(p.Position.X);
                int y = canvas.Height - (int)(p.Position.Y);

                canvas[x, y] = ColorF.Presets.Red;
            }

            canvas.SaveToPpm(@"D:\temp\test.ppm");
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