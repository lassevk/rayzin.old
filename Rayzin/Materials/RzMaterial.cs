using JetBrains.Annotations;

using Rayzin.Objects.LightSources;
using Rayzin.Primitives;

namespace Rayzin.Materials
{
    public abstract class RzMaterial
    {
        public abstract RzColor Lighting(RzPointLight light, RzPoint point, RzVector eyeVector, RzVector normalVector);
    }
}
