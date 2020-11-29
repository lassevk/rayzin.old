﻿using Rayzin.Primitives;

namespace Rayzin.Objects
{
    public abstract class RzObject
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
    }
}