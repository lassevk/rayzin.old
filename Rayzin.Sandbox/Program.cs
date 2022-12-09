using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Windows.Forms;

using Rayzin.Materials;
using Rayzin.Objects.LightSources;
using Rayzin.Objects.Renderables;
using Rayzin.Primitives;

using SkiaSharp;

namespace Rayzin.Sandbox
{
    class Program
    {
        static void Main()
        {
            using (var fm = new Form())
            {
                var canvas = new RzCanvas(1024, 1024);

                RzPoint origin = (0, 0, -5);
                double wallZ = 10;
                double wallSize = 7;
                var pixelSize = wallSize / canvas.Width;
                var half = wallSize / 2;

                var shape = new RzSphere
                {
                    Material = new RzPhongMaterial { Color = (1, 0.2, 1) }, Transformation = RzTransforms.None.Translate(0, 0, 0)
                };

                RzPoint lightPosition = (-10, 10, -10);
                RzColor lightColor = (1, 1, 1);
                RzPointLight light = new(lightPosition, lightColor);

                for (var y = 0; y < canvas.Height; y++)
                {
                    var worldY = half - pixelSize * y;
                    for (var x = 0; x < canvas.Width; x++)
                    {
                        var worldX = -half + pixelSize * x;
                        var position = new RzPoint(worldX, worldY, wallZ);
                        var r = new RzRay(origin, (position - origin).Normalize());
                        var xs = shape.Intersect(r);
                        RzIntersection? hit = xs.Hit();
                        if (hit.HasValue)
                        {
                            RzPoint point = r.Position(hit.Value.Time);
                            RzVector normal = hit.Value.Object.NormalAt(point);
                            RzVector eye = -r.Direction;

                            RzColor color = hit.Value.Object.Material.Lighting(light, point, eye, normal);
                            canvas[x, y] = color;
                        }
                    }
                }

                SKBitmap bitmap = canvas.ToBitmap();
                using (FileStream stream = File.Create(@"D:\Temp\test.png"))
                    bitmap.Encode(stream, SKEncodedImageFormat.Png, 100);
            }
        }
    }
}