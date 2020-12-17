using System;

using Rayzin.Primitives;

namespace Rayzin.Objects.LightSources
{
    public class RzPointLight : RzLightSource, IEquatable<RzPointLight>
    {
        public RzPointLight(RzPoint position, RzColor intensity) => (Position, Intensity) = (position, intensity);

        public RzPoint Position { get; }

        public RzColor Intensity { get; }

        public bool Equals(RzPointLight other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Position.Equals(other.Position) && Intensity.Equals(other.Intensity);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((RzPointLight) obj);
        }

        public override int GetHashCode() => HashCode.Combine(Position, Intensity);

        public static bool operator ==(RzPointLight left, RzPointLight right) => Equals(left, right);

        public static bool operator !=(RzPointLight left, RzPointLight right) => !Equals(left, right);
    }
}