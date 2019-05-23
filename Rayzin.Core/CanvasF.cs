using System;
using System.IO;
using System.Text;

using JetBrains.Annotations;

namespace Rayzin.Core
{
    public class CanvasF
    {
        [NotNull]
        private readonly ColorF[,] _Pixels;

        public CanvasF(int width, int height)
        {
            if (width < 1)
                throw new ArgumentOutOfRangeException(nameof(width), "width must be at least 1");

            if (height < 1)
                throw new ArgumentOutOfRangeException(nameof(height), "height must be at least 1");

            _Pixels = new ColorF[width, height];
            Width = width;
            Height = height;
        }

        public ColorF this[int x, int y]
        {
            get
            {
                if (x < 0 || y < 0 || x >= Width || y >= Height)
                    return ColorF.Presets.Black;

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
                    ColorF color = this[x, y].Clamp();
                    appendComponent(color.Red);
                    appendComponent(color.Green);
                    appendComponent(color.Blue);
                }

                appendLine();
            }

            appendLine();

            return result.ToString();
        }

        public void SaveToPpm([NotNull] string filename)
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            File.WriteAllText(filename, ToPpm(), Encoding.ASCII);
        }

        public void Clear() => Clear(ColorF.Presets.Black);

        public void Clear(ColorF color)
        {
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    _Pixels[x, y] = color;
        }
    }
}