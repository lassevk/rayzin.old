using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Threading;

using Rayzin.Materials;
using Rayzin.Objects;
using Rayzin.Objects.LightSources;
using Rayzin.Objects.Renderables;
using Rayzin.Primitives;

namespace Rayzin
{
    public class RzWorld
    {
        public RzWorld()
        {
            Objects.Add(new RzPointLight((-10, 10, -10), RzColor.Presets.White));
            Objects.Add(new RzSphere { Material = new RzPhongMaterial { Color = (0.8, 1.0, 0.6), Diffuse = 0.7, Specular = 0.2 } });
            Objects.Add(new RzSphere { Transformation = RzTransforms.None.Scale(0.5) });
        }

        public List<RzObject> Objects { get; } = new();

        public RzIntersectionsCollection Intersect(RzRay ray)
        {
            var intersections = new List<RzIntersection>();
            foreach (RzRenderable renderable in Objects.OfType<RzRenderable>())
                intersections.AddRange(renderable.Intersect(ray));

            intersections.Sort((i1, i2) => i1.Time.CompareTo(i2.Time));
            return new RzIntersectionsCollection(intersections);
        }
    }
}
