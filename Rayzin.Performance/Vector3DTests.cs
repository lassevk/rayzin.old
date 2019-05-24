using System;

using Rayzin.Primitives;

namespace Rayzin.Performance
{
    internal class Vector3DTests
    {
        public void Run()
        {
            NormalizeAlreadyNormalized();
            NormalizeDenormalized();
        }

        private void NormalizeAlreadyNormalized()
        {
            var v = new Vector3D(1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 1 / Math.Sqrt(3));
            for (int index = 0; index < 1000000; index++)
                v.Normalize();
        }

        private void NormalizeDenormalized()
        {
            var v = new Vector3D(3, 3, 3);
            for (int index = 0; index < 1000000; index++)
                v.Normalize();
        }
    }
}