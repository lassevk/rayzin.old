using System;

using Rayzin.Primitives;

namespace Rayzin.Materials
{
    public class RzPhongMaterial : RzMaterial, IEquatable<RzPhongMaterial>
    {
        public RzColor Color = RzColor.Presets.White;
        public double Ambient = 0.1;
        public double Diffuse = 0.9;
        public double Specular = 0.9;
        public double Shininess = 200.0;

        public bool Equals(RzPhongMaterial other)
        {
            if (ReferenceEquals(null, other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Color.Equals(other.Color)
                && Ambient.Equals(other.Ambient)
                && Diffuse.Equals(other.Diffuse)
                && Specular.Equals(other.Specular)
                && Shininess.Equals(other.Shininess);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (obj.GetType() != GetType())
                return false;

            return Equals((RzPhongMaterial)obj);
        }

        public override int GetHashCode() => throw new NotSupportedException();

        public static bool operator ==(RzPhongMaterial left, RzPhongMaterial right) => Equals(left, right);

        public static bool operator !=(RzPhongMaterial left, RzPhongMaterial right) => !Equals(left, right);
    }
}