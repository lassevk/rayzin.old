using System;

using Rayzin.Objects.LightSources;
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
        public override RzColor Lighting(RzPointLight light, RzPoint point, RzVector eyeVector, RzVector normalVector)
        {
            RzColor effectiveColor = Color * light.Intensity;
            RzVector lightVector = (light.Position - point).Normalize();
            RzColor ambient = effectiveColor * Ambient;
            var lightDotNormal = lightVector.Dot(normalVector);

            RzColor diffuse;
            RzColor specular;
            if (lightDotNormal < 0)
            {
                diffuse = RzColor.Presets.Black;
                specular = RzColor.Presets.Black;
            }
            else
            {
                diffuse = effectiveColor * Diffuse * lightDotNormal;
                RzVector reflectVector = (-lightVector).Reflect(normalVector);
                var reflectDotEye = reflectVector.Dot(eyeVector);
                if (reflectDotEye <= 0)
                    specular = RzColor.Presets.Black;
                else
                {
                    var factor = Math.Pow(reflectDotEye, Shininess);
                    specular = light.Intensity * Specular * factor;
                }
            }

            return ambient + diffuse + specular;
        }

        public static bool operator ==(RzPhongMaterial left, RzPhongMaterial right) => Equals(left, right);

        public static bool operator !=(RzPhongMaterial left, RzPhongMaterial right) => !Equals(left, right);
    }
}