using System;

using Rayzin.Primitives;

namespace Rayzin.Performance
{
    internal class RzVectorTests
    {
        public void Run()
        {
            NormalizeAlreadyNormalized();
            NormalizeDenormalized();
        }

        private void NormalizeAlreadyNormalized()
        {
            var v = new RzVector(1 / Math.Sqrt(3), 1 / Math.Sqrt(3), 1 / Math.Sqrt(3));
            for (int index = 0; index < 1000000; index++)
                v.Normalize();
        }

        private void NormalizeDenormalized()
        {
            var v = new RzVector(3, 3, 3);
            for (int index = 0; index < 1000000; index++)
                v.Normalize();
        }
    }
}