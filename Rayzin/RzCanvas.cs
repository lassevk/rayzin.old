using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

using JetBrains.Annotations;

using Rayzin.Primitives;

using SkiaSharp;

namespace Rayzin
{
    public class RzCanvas
    {
        [NotNull]
        private readonly RzColor[,] _Pixels;

        public RzCanvas(int width, int height)
        {
            if (width < 1)
                throw new ArgumentOutOfRangeException(nameof(width), "width must be at least 1");

            if (height < 1)
                throw new ArgumentOutOfRangeException(nameof(height), "height must be at least 1");

            _Pixels = new RzColor[width, height];
            Width = width;
            Height = height;
        }

        public RzColor this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    return RzColor.Presets.Black;

                return _Pixels[x, y];
            }
            set
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    return;

                _Pixels[x, y] = value;
            }
        }

        public int Width { get; }

        public int Height { get; }

        [NotNull]
        public string ToPpm()
        {
            var result = new StringBuilder();
            result.AppendLine("P3");
            result.AppendLine($"{Width} {Height}");
            result.AppendLine("255");

            var line = new StringBuilder();

            void appendLine()
            {
                if (line.Length > 0)
                {
                    result.AppendLine(line.ToString());
                    line.Clear();
                }
            }

            void appendComponent(double component)
            {
                string componentString = ((int)(component * 255 + 0.5)).ToString();
                if (line.Length + 1 + componentString.Length > 70)
                {
                    appendLine();
                    line.Append(componentString);
                }
                else
                {
                    if (line.Length > 0)
                        line.Append(' ');

                    line.Append(componentString);
                }
            }

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    RzColor color = this[x, y].Clamp();
                    appendComponent(color.Red);
                    appendComponent(color.Green);
                    appendComponent(color.Blue);
                }

                appendLine();
            }

            appendLine();

            return result.ToString();
        }

        public SKBitmap ToBitmap()
        {
            var bitmap = new SKBitmap(Width, Height, SKColorType.Rgb888x, SKAlphaType.Opaque);
            for (var y = 0; y < Height; y++)
                for (var x = 0; x < Width; x++)
                    bitmap.SetPixel(x, y, this[x, y].ToColor());

            return bitmap;
        }

        public void SaveToPpm([NotNull] string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            File.WriteAllText(filename, ToPpm(), Encoding.ASCII);
        }

        public void Clear() => Clear(RzColor.Presets.Black);

        public void Clear(RzColor color)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _Pixels[x, y] = color;
        }
    }
}