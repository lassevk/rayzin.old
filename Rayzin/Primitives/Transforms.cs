using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rayzin.Primitives
{
    public static class Transforms
    {
        public static MatrixF Translation(double x, double y, double z) => new MatrixF(4, 1, 0, 0, x, 0, 1, 0, y, 0, 0, 1, z, 0, 0, 0, 1);
    }
}
