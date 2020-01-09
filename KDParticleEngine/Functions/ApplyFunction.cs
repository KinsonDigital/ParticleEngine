using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace KDParticleEngine.Functions
{
    public class ApplyFunction : IApplyFunction
    {
        public void Apply(float value, Particle particle, ApplyType applyTo)
        {
            switch (applyTo)
            {
                case ApplyType.X:
                    particle.Position = new PointF(value, particle.Position.Y);
                    break;
                default:
                    break;
            }
        }
    }
}
