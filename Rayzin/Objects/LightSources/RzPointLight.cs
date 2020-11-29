using Rayzin.Primitives;

namespace Rayzin.Objects.LightSources
{
    public class RzPointLight : RzLightSource
    {
        public RzPointLight(RzPoint position, RzColor intensity) => (Position, Intensity) = (position, intensity);

        public RzPoint Position { get; }

        public RzColor Intensity { get; }
    }
}