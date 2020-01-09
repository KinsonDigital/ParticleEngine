using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Functions
{
    public interface IApplyFunction
    {
        void Apply(float value, Particle particle, ApplyType applyTo);
    }
}
