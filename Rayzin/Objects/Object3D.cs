using Rayzin.Primitives;

namespace Rayzin.Objects
{
    public abstract class Object3D
    {
        private MatrixF _Transformation = MatrixF.Presets.Identity4;
        private MatrixF _InverseTransformation = MatrixF.Presets.Identity4Inverse;

        public abstract Intersections Intersect(RayF ray);

        public MatrixF Transformation
        {
            get => _Transformation;
            set => (_Transformation, _InverseTransformation) = (value, value.Inverse());
        }

        public MatrixF InverseTransformation => _InverseTransformation;
    }
}