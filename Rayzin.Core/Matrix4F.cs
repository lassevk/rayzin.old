using JetBrains.Annotations;

namespace Rayzin.Core
{
    public unsafe struct Matrix4F
    {
        [NotNull]
        private fixed double _Values[16];

        public Matrix4F(params double[] values)
        {
            for (int index = 0; index < 16; index++)
                _Values[index] = values[index];
        }

        public unsafe double this[int x, int y]
        {
            get => _Values[x * 4 + y];
            set => _Values[x * 4 + y] = value;
        }
    }
}