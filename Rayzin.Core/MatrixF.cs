using System;
using System.Text;

using JetBrains.Annotations;

namespace Rayzin.Core
{
    public unsafe struct MatrixF
    {
        [NotNull]
        private readonly double[] _Values;

        public MatrixF(int size, [NotNull] double[] values)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (values.Length != size * size)
                throw new ArgumentOutOfRangeException(
                    nameof(values), $"values must have a length corresponding to size*size (={size * size}) but was {values.Length}");

            _Values = new double[size * size];
            for (int index = 0; index < size * size; index++)
                _Values[index] = values[index];

            Size = size;
        }

        public int Size { get; }

        public unsafe double this[int x, int y]
        {
            get => _Values[x * Size + y];
            set => _Values[x * Size + y] = value;
        }

        public override string ToString()
        {
            var result = new StringBuilder();
            for (int y = 0; y < Size; y++)
            {
                if (result.Length > 0)
                    result.AppendLine("");

                result.Append("| ");
                for (int x = 0; x < Size; x++)
                {
                    if (x > 0)
                        result.Append(" | ");
                    result.Append(this[y, x]);
                }

                result.Append(" |");
            }

            return result.ToString();
        }
    }
}