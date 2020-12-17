using System;

using Rayzin.Materials;
using Rayzin.Primitives;

namespace Rayzin.Objects.Renderables
{
    public abstract class RzRenderable : RzObject, IEquatable<RzRenderable>
    {
        private RzMatrix _Transformation = RzMatrix.Presets.Identity4;
        private RzMatrix _InverseTransformation = RzMatrix.Presets.Identity4Inverse;

        public abstract RzIntersectionsCollection Intersect(RzRay ray);

        public RzMatrix Transformation
        {
            get => _Transformation;
            set => (_Transformation, _InverseTransformation) = (value, value.Inverse());
        }

        public RzMatrix InverseTransformation => _InverseTransformation;

        public RzMaterial Material { get; set; } = new RzPhongMaterial();

        public abstract RzVector NormalAt(RzPoint worldPoint);

        public bool Equals(RzRenderable other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return _Transformation.Equals(other._Transformation) && _InverseTransformation.Equals(other._InverseTransformation) && Equals(Material, other.Material);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((RzRenderable) obj);
        }

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzRenderable left, RzRenderable right) => Equals(left, right);

        public static bool operator !=(RzRenderable left, RzRenderable right) => !Equals(left, right);
    }
}
